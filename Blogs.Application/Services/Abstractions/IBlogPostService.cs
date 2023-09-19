using Blogs.Application.DTO.BlogPost;
using Blogs.Application.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.Services.Abstractions
{
    public interface IBlogPostService
    {
        Task<BlogPostResultDTO> GetAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<BlogPostResultDTO>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<BlogPostResultDTO> CreateAsync(CreateBlogPostDTO blogPost, UserDTO user, CancellationToken cancellationToken = default);
        Task<BlogPostResultDTO> UpdateAsync(UpdateBlogPostDTO comment, UserDTO user, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
