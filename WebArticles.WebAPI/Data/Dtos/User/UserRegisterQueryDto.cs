using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class UserRegisterQueryDto
    {
        [MinLength(2)]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [MinLength(2)]
        [MaxLength(30)]
        public string LastName { get; set; }


        [MinLength(4)]
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
