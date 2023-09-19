using Blogs.Application.DTO.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators.User
{
    public class LoginValidator : AbstractValidator<LoginRequestDTO>
    {
        public LoginValidator() 
        {
            RuleSet("UsernameAndPassword", () =>
            {
                RuleFor(x => x.Username).NotNull().NotEmpty()
                    .WithMessage("Username must not be empty")
                    .MinimumLength(16)
                    .WithMessage("Username must be at least 16 characters long");
                RuleFor(x => x.Password).NotNull().NotEmpty()
                    .WithMessage("Password must not be empty")
                    .MinimumLength(8)
                    .WithMessage("Password must be at least 8 characters long");
            });
        }
    }
}
