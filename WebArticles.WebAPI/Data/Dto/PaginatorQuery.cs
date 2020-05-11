using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Data.Dto
{
    public class PaginatorQuery
    {
        // Filters
        public string Topics { get; set; }

        public int? MinRating { get; set; }

        public int? MaxRating { get; set; }

        public DateTime? MinDate { get; set; }

        public DateTime? MaxDate { get; set; }

        public string Tags { get; set; }

        public Filters Filters
        {
            get
            {
                return new Filters()
                {
                    Topics = this.Topics?.Substring(1, this.Topics.Length - 2).Replace("\"","").Split(","),
                    MaxRating = this.MaxRating,
                    MinRating = this.MinRating,
                    MaxDate = this.MaxDate,
                    MinDate = this.MinDate,
                    Tags = this.Tags
                };
            }
        }

        // Search
        public string Search { get; set; } = "";

        // Sorting
        public string SortBy { get; set; } = "id";
        public string SortDirection { get; set; } = "asc";
        
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
        
    }
}
