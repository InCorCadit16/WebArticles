using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class Filters
    {
        public string[] Topics;

        public int? MinRating, MaxRating;

        public DateTime? MinDate;
        public DateTime? MaxDate;

        public string Tags;

        public bool IsEmpty { get
            {
                return Topics == null && MinRating == null && MaxRating == null &&
                    MinDate == null && MaxDate == null && Tags == null;
            } 
        }
    }
}
