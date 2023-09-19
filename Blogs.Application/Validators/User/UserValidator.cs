using Blogs.Application.DTO.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators.User
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator() 
        { 
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("User Id can't be empty");
            RuleFor(x => x.Username).NotNull().NotEmpty().WithMessage("Username can't be empty");
        }
    }
}
