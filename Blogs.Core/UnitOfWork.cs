using Blogs.Core.DataContexts;
using Blogs.Core.Repositories;
using Blogs.Core.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly BlogPostsApiContext _context;
        private bool _disposed;

        public IUserRepository UserRepository { get; }
        public IBlogPostsRepository BlogPostsRepository { get; }
        public ICommentsRepository CommentRepository { get; }

        public UnitOfWork(BlogPostsApiContext context, IUserRepository userRepository,
            IBlogPostsRepository blogPostsRepository,
            ICommentsRepository commentsRepository)
        {
            _context = context;
            UserRepository = userRepository;
            BlogPostsRepository = blogPostsRepository;
            CommentRepository = commentsRepository;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Rollback()
        {
            foreach (var entry in _context.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
