using Blogs.Application.DTO.BlogPost;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Validators.BlogPost
{
    public class CreateBlogPostValidator : AbstractValidator<CreateBlogPostDTO>
    {
        public CreateBlogPostValidator() 
        { 
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("The Blog must have a Title");
            RuleFor(x => x.HtmlContent).NotNull().NotEmpty().WithMessage("The Blog must have Content");
        }
    }
}
