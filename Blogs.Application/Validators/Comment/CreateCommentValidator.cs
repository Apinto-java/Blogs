using Blogs.Application.DTO.Comment;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators.Comment
{
    public class CreateCommentValidator : AbstractValidator<CreateCommentDTO>
    {
        public CreateCommentValidator() 
        { 
            RuleFor(x => x.BlogPostId).NotNull().NotEmpty().WithMessage("Must specify the Blog");
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Text must not be empty");
        }
    }
}
