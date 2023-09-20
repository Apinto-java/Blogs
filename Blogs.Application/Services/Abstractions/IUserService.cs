using Blogs.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Abstractions
{
    public interface IUserService
    {
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginDTO, CancellationToken cancellationToken = default);
        Task RegisterAsync(RegisterRequestDTO registerDTO, CancellationToken cancellationToken = default);
    }
}
