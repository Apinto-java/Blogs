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
        private readonly AbstractValidator<UserDTO> _userValidator;

        public CommentService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            AbstractValidator<CreateCommentDTO> createValidator, 
            AbstractValidator<UpdateCommentDTO> updateValidator, 
            AbstractValidator<UserDTO> userValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _userValidator = userValidator;
        }

        public async Task CreateAsync(CreateCommentDTO comment, UserDTO user, CancellationToken cancellationToken = default)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var validation = await _createValidator.ValidateAsync(comment);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            var userValidation = await _userValidator.ValidateAsync(user);

            if (!userValidation.IsValid)
                throw new BusinessException(string.Join("\n", userValidation.Errors));

            var commentModel = _mapper.Map<Comment>(comment);
            commentModel.CreationDate = DateTime.Now;
            commentModel.CreationUser = user.Id;
            commentModel.UpdateDate = DateTime.Now;
            commentModel.UpdateUser = user.Id;

            _unitOfWork.CommentRepository.Insert(commentModel);
            _unitOfWork.Commit();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            _unitOfWork.CommentRepository.Delete(id);
            _unitOfWork.Commit();
        }

        public async Task<IEnumerable<CommentResultDTO>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return _unitOfWork.CommentRepository.GetAll().Select(comment => _mapper.Map<CommentResultDTO>(comment));
        }

        public async Task<CommentResultDTO> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _mapper.Map<CommentResultDTO>(_unitOfWork.CommentRepository.Get(id));
        }

        public async Task UpdateAsync(UpdateCommentDTO comment, UserDTO user, CancellationToken cancellationToken = default)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));

            if (user == null)
                throw new ArgumentNullException(nameof(user));

            var validation = await _updateValidator.ValidateAsync(comment);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            var userValidation = await _userValidator.ValidateAsync(user);

            if (!userValidation.IsValid)
                throw new BusinessException(string.Join("\n", userValidation.Errors));

            var commentToUpdate = _unitOfWork.CommentRepository.Get(comment.Id);
            commentToUpdate.UpdateDate = DateTime.Now;
            commentToUpdate.UpdateUser = user.Id;

            _unitOfWork.CommentRepository.Update(_mapper.Map(comment, commentToUpdate));

            _unitOfWork.Commit();
        }
    }
}
