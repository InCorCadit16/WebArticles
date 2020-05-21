using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebArticles.WebAPI.Infrastructure.Models
{
    public class RequestFilters
    {
        public FiltersLogicalOperators LogicalOperator { get; set; }
        public IList<Filter> Filters;
        public IList<string> Tags;
    }
}
