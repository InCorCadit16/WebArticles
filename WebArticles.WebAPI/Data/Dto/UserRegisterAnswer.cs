using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class UserRegisterAnswer
    {
        public User User { get; set; }

        public string ErrorMessage { get; set; }
    }
}
