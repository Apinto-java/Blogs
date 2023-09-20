using AutoMapper;
using Blogs.Application.DTO.BlogPost;
using Blogs.Application.DTO.User;
using Blogs.Application.Exceptions;
using Blogs.Application.Services.Abstractions;
using Blogs.Core;
using Blogs.Core.Models;
using FluentValidation;
using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Implementations
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHtmlSanitizer _sanitizer;
        private readonly AbstractValidator<CreateBlogPostDTO> _createValidator;
        private readonly AbstractValidator<UpdateBlogPostDTO> _updateValidator; 

        public BlogPostService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            IHtmlSanitizer sanitizer, 
            AbstractValidator<CreateBlogPostDTO> createValidator,
            AbstractValidator<UpdateBlogPostDTO> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sanitizer = sanitizer;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<BlogPostResultDTO> CreateAsync(CreateBlogPostDTO blogPost, Guid userId, CancellationToken cancellationToken = default)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            if(userId == null)
                throw new ArgumentNullException(nameof(userId));

            var validation = await _createValidator.ValidateAsync(blogPost);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            if (!_unitOfWork.UserRepository.Exists(userId))
                throw new BusinessException("User does not exist");

            blogPost.HtmlContent = _sanitizer.Sanitize(blogPost.HtmlContent);

            var blogPostModel = _mapper.Map<BlogPost>(blogPost);
            blogPostModel.Id = Guid.NewGuid();
            blogPostModel.CreationDate = DateTime.Now;
            blogPostModel.CreationUser = userId;
            blogPostModel.UpdateDate = DateTime.Now;
            blogPostModel.UpdateUser = userId;

            _unitOfWork.BlogPostsRepository.Insert(blogPostModel);
            _unitOfWork.Commit();

            var result = _mapper.Map<BlogPostResultDTO>(blogPostModel);

            return result;
        }

        public async Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var blogPost = _unitOfWork.BlogPostsRepository.Get(id);

            if (blogPost.CreationUser != userId)
                throw new BusinessException("You can't delete this post, you are not its author");

            _unitOfWork.BlogPostsRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<BlogPostResultDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _unitOfWork.BlogPostsRepository.GetAll()
                .Select(blogPost => _mapper.Map<BlogPostResultDTO>(blogPost));
        }

        public async Task<BlogPostResultDTO> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var blogPost = _unitOfWork.BlogPostsRepository.Get(id);
            var result = _mapper.Map<BlogPostResultDTO>(blogPost);

            return result;
        }

        public async Task<BlogPostResultDTO> UpdateAsync(UpdateBlogPostDTO blogPost, Guid userId, CancellationToken cancellationToken = default)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var validation = await _updateValidator.ValidateAsync(blogPost);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            if (!_unitOfWork.UserRepository.Exists(userId))
                throw new BusinessException("User does not exist");

            blogPost.HtmlContent = _sanitizer.Sanitize(blogPost.HtmlContent);

            var blogPostToUpdate = _unitOfWork.BlogPostsRepository.Get(blogPost.Id);

            if (blogPostToUpdate.CreationUser != userId)
                throw new BusinessException("You can't edit this Post, you are not its author");

            blogPostToUpdate.UpdateDate = DateTime.Now;
            blogPostToUpdate.UpdateUser = userId;

            _unitOfWork.BlogPostsRepository.Update(_mapper.Map(blogPost, blogPostToUpdate));

            _unitOfWork.Commit();

            var result = _mapper.Map<BlogPostResultDTO>(blogPostToUpdate);

            return result;
        }
    }
}
