using Blogs.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Abstractions
{
    public interface IJwtService
    {
        LoginResultDTO GenerateJWT(Guid userId);
    }
}
