using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.DTO.Comment
{
    public class UpdateCommentDTO
    {
        [Required]
        public string Text {  get; set; }
    }
}
