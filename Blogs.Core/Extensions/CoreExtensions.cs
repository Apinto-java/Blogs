using Blogs.Core.DataContexts;
using Blogs.Core.Repositories;
using Blogs.Core.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Extensions
{
    public static class CoreExtensions
    {
        public static IServiceCollection AddCoreLayer(this IServiceCollection services, IConfiguration configuration) 
        {
            var connectionString = configuration.GetConnectionString("Local");
            services.AddDbContext<BlogPostsApiContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBlogPostsRepository, BlogPostsRepository>();
            services.AddScoped<ICommentsRepository, CommentsRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
