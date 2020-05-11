using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class RatingUpdate
    {
        public long Id { get; set; }

        public int Rating { get; set; }
    }
}
