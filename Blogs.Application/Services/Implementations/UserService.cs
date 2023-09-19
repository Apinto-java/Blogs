using AngleSharp.Html.Dom;
using AutoMapper;
using Blogs.Application.DTO.Comment;
using Blogs.Application.DTO.User;
using Blogs.Application.Exceptions;
using Blogs.Application.Services.Abstractions;
using Blogs.Core;
using Blogs.Core.Models;
using Blogs.Utils.EncryptionDecryption;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AbstractValidator<RegisterRequestDTO> _registerValidator;
        private readonly AbstractValidator<LoginRequestDTO> _loginValidator;

        public UserService(IUnitOfWork unitOfWork, 
            IMapper mapper, 
            AbstractValidator<RegisterRequestDTO> registerValidator, 
            AbstractValidator<LoginRequestDTO> loginValidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _registerValidator = registerValidator;
            _loginValidator = loginValidator;
        }

        public async Task LoginAsync(LoginRequestDTO loginDTO, CancellationToken cancellationToken = default)
        {
            if(loginDTO == null)
                throw new ArgumentNullException(nameof(loginDTO));

            var validation = await _loginValidator.ValidateAsync(loginDTO);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            var userModel = _unitOfWork.UserRepository.GetByUsername(loginDTO.Username);

            if (userModel == null)
                throw new BusinessException("Incorrect user or password");

            var passwordEncrypted = Encrypt.EncryptText(loginDTO.Password);

            if (System.Text.Encoding.UTF8.GetBytes(passwordEncrypted) != userModel.Password)
                throw new BusinessException("Incorrect user or password");

            //TODO: When the user exists
        }

        public async Task RegisterAsync(RegisterRequestDTO registerDTO, CancellationToken cancellationToken = default)
        {
            if (registerDTO == null)
                throw new ArgumentNullException(nameof(registerDTO));

            var validation = await _registerValidator.ValidateAsync(registerDTO);

            if (!validation.IsValid)
                throw new BusinessException(string.Join("\n", validation.Errors));

            var userModel = _unitOfWork.UserRepository.GetByUsername(registerDTO.Username);

            if (userModel != null)
                throw new BusinessException("The user already exists");

            userModel.Id = Guid.NewGuid();
            userModel.Password = System.Text.Encoding.UTF8.GetBytes(Encrypt.EncryptText(registerDTO.Password));
            userModel.CreationDate = DateTime.Now;
            userModel.CreationUser = userModel.Id;
            userModel.UpdateDate = DateTime.Now;
            userModel.UpdateUser = userModel.Id;

            _unitOfWork.UserRepository.Insert(userModel);
            _unitOfWork.Commit();
        }
    }
}
