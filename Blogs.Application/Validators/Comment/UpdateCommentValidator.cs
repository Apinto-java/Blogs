using Blogs.Application.DTO.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators.Comment
{
    public class UpdateCommentValidator : AbstractValidator<UpdateCommentDTO>
    {
        public UpdateCommentValidator() 
        {
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Text must not be empty");
        }
    }
}
