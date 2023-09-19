using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Application.DTO.Comment;
using Blogs.Application.DTO.User;

namespace Blogs.Application.Services.Abstractions
{
    public interface ICommentService
    {
        Task<CommentResultDTO> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CommentResultDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CommentResultDTO> CreateAsync(CreateCommentDTO comment, UserDTO user, CancellationToken cancellationToken = default);
        Task<CommentResultDTO> UpdateAsync(UpdateCommentDTO comment, UserDTO user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
