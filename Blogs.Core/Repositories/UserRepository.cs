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
    public class UserRepository : RepositoryBase<User, Guid>, IUserRepository
    {
        public UserRepository(BlogPostsApiContext context) : base(context)
        {
        }

        public User GetByUsername(string username)
        {
            return _dbSet.FirstOrDefault(x => x.Username == username);
        }
    }
}
