using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class UserLoginQueryDto
    {
        [MinLength(4)]
        [MaxLength(30)]
        [Required]
        public string UserName { get; set; }

        [MinLength(8)]
        [MaxLength(30)]
        [Required]
        public string Password { get; set; }
    }
}
