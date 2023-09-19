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
        public string Title { get; set; }
        public Guid BlogPostId { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreationUsername { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public string UpdateUsername { get; set; }
    }
}
