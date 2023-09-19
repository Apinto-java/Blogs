using Blogs.Core.DataContexts;
using Blogs.Core.Models;
using Blogs.Core.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Repositories
{
    public class CommentsRepository : RepositoryBase<Comment, Guid>, ICommentsRepository
    {
        public CommentsRepository(BlogPostsApiContext context) : base(context)
        {
        }
    }
}
