using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Application.DTO.User
{
    public class RegisterRequestDTO
    {
        [Required, MinLength(8)]
        public string Username { get; set; }
        [Required, MinLength(8)]
        public string Password { get; set; }
    }
}
