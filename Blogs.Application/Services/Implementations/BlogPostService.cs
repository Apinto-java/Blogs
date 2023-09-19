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
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Implementations
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly HtmlSanitizer _sanitizer;
        private readonly AbstractValidator<CreateBlogPostDTO> _createValidator;
        private readonly AbstractValidator<UpdateBlogPostDTO> _updateValidator;
        private readonly AbstractValidator<UserDTO> _userValidator; 

        public BlogPostService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            HtmlSanitizer sanitizer, 
            AbstractValidator<CreateBlogPostDTO> createValidator,
            AbstractValidator<UpdateBlogPostDTO> updateValidator,
            AbstractValidator<UserDTO> userValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sanitizer = sanitizer;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _userValidator = userValidator;
        }

        public async Task<BlogPostResultDTO> CreateAsync(CreateBlogPostDTO blogPost, UserDTO user, CancellationToken cancellationToken = default)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var validation = await _createValidator.ValidateAsync(blogPost);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            var userValidation = await _userValidator.ValidateAsync(user);

            if (!userValidation.IsValid)
                throw new BusinessException(string.Join("\n", userValidation.Errors));

            blogPost.HtmlContent = _sanitizer.Sanitize(blogPost.HtmlContent);

            var blogPostModel = _mapper.Map<BlogPost>(blogPost);
            blogPostModel.CreationDate = DateTime.Now;
            blogPostModel.CreationUser = user.Id;
            blogPostModel.UpdateDate = DateTime.Now;
            blogPostModel.UpdateUser = user.Id;

            _unitOfWork.BlogPostsRepository.Insert(blogPostModel);
            _unitOfWork.Commit();

            return _mapper.Map<BlogPostResultDTO>(blogPostModel);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _unitOfWork.BlogPostsRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<BlogPostResultDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _unitOfWork.BlogPostsRepository.GetAll().Select(blogPost => _mapper.Map<BlogPostResultDTO>(blogPost));
        }

        public async Task<BlogPostResultDTO> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<BlogPostResultDTO>(_unitOfWork.BlogPostsRepository.Get(id));
        }

        public async Task<BlogPostResultDTO> UpdateAsync(UpdateBlogPostDTO blogPost, UserDTO user, CancellationToken cancellationToken = default)
        {
            if (blogPost == null)
                throw new ArgumentNullException(nameof(blogPost));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var validation = await _updateValidator.ValidateAsync(blogPost);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            var userValidation = await _userValidator.ValidateAsync(user);

            if (!userValidation.IsValid)
                throw new BusinessException(string.Join("\n", userValidation.Errors));

            blogPost.HtmlContent = _sanitizer.Sanitize(blogPost.HtmlContent);

            var blogPostToUpdate = _unitOfWork.BlogPostsRepository.Get(blogPost.Id);
            blogPostToUpdate.UpdateDate = DateTime.Now;
            blogPostToUpdate.UpdateUser = user.Id;

            _unitOfWork.BlogPostsRepository.Update(_mapper.Map(blogPost, blogPostToUpdate));

            _unitOfWork.Commit();

            return _mapper.Map<BlogPostResultDTO>(blogPostToUpdate);
        }
    }
}
