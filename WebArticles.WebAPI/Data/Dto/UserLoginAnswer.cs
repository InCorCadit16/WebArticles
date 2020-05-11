using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class UserLoginAnswer
    {
        public string EncodedToken { get; set; }

        public long UserId { get; set; }

        public string ErrorMessage { get; set; }
    }
}
