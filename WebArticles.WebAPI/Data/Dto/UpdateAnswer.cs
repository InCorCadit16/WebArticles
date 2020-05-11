using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class UpdateAnswer
    {
        public bool Succeeded { get; set; }
        
        public string Error { get; set; }
    }
}
