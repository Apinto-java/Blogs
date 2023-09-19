using Blogs.Core.DataContexts;
using Blogs.Core.Models;
using Blogs.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Repositories
{
    public class BlogPostsRepository : RepositoryBase<BlogPost, Guid>, IBlogPostsRepository
    {
        public BlogPostsRepository(BlogPostsApiContext context) : base(context)
        {
        }
    }
}
