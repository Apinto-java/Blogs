using Blogs.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Core.Repositories.Abstractions
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        User GetByUsername(string username);
    }
}
