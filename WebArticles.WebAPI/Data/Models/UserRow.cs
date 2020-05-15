using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Models
{
    public class UserRow
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Articles { get; set; }
        public int WriterRating { get; set; }
        public int Reviewes { get; set; }
        public int ReviewerRating { get; set; }
    }
}
