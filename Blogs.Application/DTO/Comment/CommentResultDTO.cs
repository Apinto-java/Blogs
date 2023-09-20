using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.DTO.Comment
{
    public class CommentResultDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid BlogPostId { get; set; }
    }
}
