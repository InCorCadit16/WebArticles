using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class UserLoginAnswerDto
    {
        public string EncodedToken { get; set; }

        public long UserId { get; set; }
    }
}
