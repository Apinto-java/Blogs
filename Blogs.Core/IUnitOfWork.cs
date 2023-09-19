using Blogs.Core.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core
{
    public interface IUnitOfWork : IDisposable
    {
        public void Commit();
        public void Rollback();
        IUserRepository UserRepository { get; }
        IBlogPostsRepository BlogPostsRepository { get; }
        ICommentsRepository CommentRepository { get; }
    }
}
