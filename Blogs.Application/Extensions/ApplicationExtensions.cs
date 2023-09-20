using Blogs.Application.DTO.BlogPost;
using Blogs.Application.DTO.Comment;
using Blogs.Application.DTO.User;
using Blogs.Application.Services.Abstractions;
using Blogs.Application.Services.Implementations;
using Blogs.Application.Validators.BlogPost;
using Blogs.Application.Validators.Comment;
using Blogs.Application.Validators.User;
using Blogs.Core.Extensions;
using FluentValidation;
using Ganss.Xss;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreLayer(configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IHtmlSanitizer, HtmlSanitizer>();

            services.AddScoped<AbstractValidator<LoginRequestDTO>, LoginValidator>();
            services.AddScoped<AbstractValidator<RegisterRequestDTO>, RegisterValidator>();
            services.AddScoped<AbstractValidator<UserDTO>, UserValidator>();

            services.AddScoped<AbstractValidator<CreateBlogPostDTO>, CreateBlogPostValidator>();
            services.AddScoped<AbstractValidator<UpdateBlogPostDTO>,  UpdateBlogPostValidator>();

            services.AddScoped<AbstractValidator<CreateCommentDTO>, CreateCommentValidator>();
            services.AddScoped<AbstractValidator<UpdateCommentDTO>, UpdateCommentValidator>();

            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<ICommentService, CommentService>();

            return services;
        }
    }
}
