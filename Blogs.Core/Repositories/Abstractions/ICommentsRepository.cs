using Blogs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Repositories.Abstractions
{
    public interface ICommentsRepository : IRepository<Comment, Guid>
    {
        IEnumerable<Comment> GetCommentsByPost(Guid postId);
    }
}
