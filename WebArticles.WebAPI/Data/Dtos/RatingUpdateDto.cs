using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dtos
{
    public class RatingUpdateDto
    {
        public long Id { get; set; }

        public int Rating { get; set; }
    }
}
