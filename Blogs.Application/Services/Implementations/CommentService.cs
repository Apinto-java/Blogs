using AutoMapper;
using Blogs.Application.DTO.BlogPost;
using Blogs.Application.DTO.Comment;
using Blogs.Application.DTO.User;
using Blogs.Application.Exceptions;
using Blogs.Application.Services.Abstractions;
using Blogs.Application.Validators.Comment;
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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<CreateCommentDTO> _createValidator;
        private readonly AbstractValidator<UpdateCommentDTO> _updateValidator;

        public CommentService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            AbstractValidator<CreateCommentDTO> createValidator, 
            AbstractValidator<UpdateCommentDTO> updateValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<CommentResultDTO> CreateAsync(CreateCommentDTO comment, Guid userId, CancellationToken cancellationToken = default)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var validation = await _createValidator.ValidateAsync(comment);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            if (!_unitOfWork.UserRepository.Exists(userId))
                throw new BusinessException("User does not exist");

            if (!_unitOfWork.BlogPostsRepository.Exists(comment.BlogPostId))
                throw new BusinessException("The blog does not exist");

            var commentModel = _mapper.Map<Comment>(comment);
            commentModel.Id = Guid.NewGuid();
            commentModel.CreationDate = DateTime.Now;
            commentModel.CreationUser = userId;
            commentModel.UpdateDate = DateTime.Now;
            commentModel.UpdateUser = userId;

            _unitOfWork.CommentRepository.Insert(commentModel);
            _unitOfWork.Commit();

            var result = _mapper.Map<CommentResultDTO>(commentModel);

            return result;
        }

        public async Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var comment = _unitOfWork.CommentRepository.Get(id);

            if (comment.CreationUser != userId)
                throw new BusinessException("You can't delete this comment, you are not its author");

            _unitOfWork.CommentRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<CommentResultDTO>> GetBlogPostComments(Guid blogPostId, 
            CancellationToken cancellationToken = default)
        {
            return _unitOfWork.CommentRepository.GetAllByBlogPost(blogPostId).Select(comment => _mapper.Map<CommentResultDTO>(comment));
        }

        public async Task<CommentResultDTO> GetCommentOfBlogPost(Guid blogPostId, Guid id, CancellationToken cancellationToken = default)
        {
            var comment = _unitOfWork.CommentRepository.GetByBlogPost(blogPostId, id);
            var result = _mapper.Map<CommentResultDTO>(comment);

            return result;
        }

        public async Task<CommentResultDTO> UpdateAsync(UpdateCommentDTO comment, Guid userId, CancellationToken cancellationToken = default)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var validation = await _updateValidator.ValidateAsync(comment);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            if (!_unitOfWork.UserRepository.Exists(userId))
                throw new BusinessException("User does not exist");

            var commentToUpdate = _unitOfWork.CommentRepository.Get(comment.Id);

            if (commentToUpdate.CreationUser != userId)
                throw new BusinessException("You can't edit this comment, you are not its author ");

            commentToUpdate.UpdateDate = DateTime.Now;
            commentToUpdate.UpdateUser = userId;

            _unitOfWork.CommentRepository.Update(_mapper.Map(comment, commentToUpdate));
            _unitOfWork.Commit();

            var result = _mapper.Map<CommentResultDTO>(commentToUpdate);

            return result;
        }
    }
}
