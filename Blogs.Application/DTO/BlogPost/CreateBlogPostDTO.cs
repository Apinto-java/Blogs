using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.DTO.BlogPost
{
    public class CreateBlogPostDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string HtmlContent { get; set; }

    }
}
