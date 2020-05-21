using System.Collections.Generic;

namespace WebArticles.WebAPI.Infrastructure.Models
{
    public class PaginatorQuery
    {
        public RequestFilters Filters { get; set; }

        public string SearchString { get; set; } = "";
        public string SortBy { get; set; } = "id";
        public string SortDirection { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        
    }
}
