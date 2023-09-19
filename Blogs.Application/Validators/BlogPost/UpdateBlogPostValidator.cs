using Blogs.Application.DTO.BlogPost;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators.BlogPost
{
    public class UpdateBlogPostValidator : AbstractValidator<UpdateBlogPostDTO>
    {
        public UpdateBlogPostValidator() 
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("The Blog must have a Title");
            RuleFor(x => x.HtmlContent).NotNull().NotEmpty().WithMessage("The Blog must have Content");
        }
    }
}
