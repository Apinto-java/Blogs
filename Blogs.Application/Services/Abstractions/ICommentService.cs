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
        Task<CommentResultDTO> GetCommentOfBlogPost(Guid blogPostId, Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<CommentResultDTO>> GetBlogPostComments(Guid blogPostId, CancellationToken cancellationToken = default);
        Task<CommentResultDTO> CreateAsync(CreateCommentDTO comment, Guid userId, CancellationToken cancellationToken = default);
        Task<CommentResultDTO> UpdateAsync(UpdateCommentDTO comment, Guid userId, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    }
}
