﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.DTO.Comment
{
    public class CreateCommentDTO
    {
        public Guid BlogPostId { get; set; }
        public string Text { get; set; }
    }
}
