﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.DTO.User
{
    public class RegisterRequestDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
