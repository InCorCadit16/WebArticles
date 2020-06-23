using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.DataModel.Entities;
using WebArticles.WebAPI.Infrastructure.Models;

namespace WebAPI.Infrastructure.Extensions
{
    public static class ListExtensions
    {

        public static List<T> GetPage<T>(this List<T> query, int p, int size)
        {
            return query.Skip((p - 1) * size).Take(size).ToList();
        }

        public static List<Article> TagsMatch(this List<Article> query, RequestFilters filters)
        {
            if (filters != null && filters.Tags != null)
            {
                if (filters.Tags.Count() > 0)
                {
                    query = query.Where(a => a.Tags.Split('#', StringSplitOptions.None).AsQueryable().Intersect(filters.Tags).Any()).ToList();
                }
            }
            return query;
        }

        public static List<Article> SortByRating(this List<Article> query, PaginatorQuery paginatorQuery)
        {
            if (!string.IsNullOrWhiteSpace(paginatorQuery.SortBy) && paginatorQuery.SortBy == "rating")
            {
                if (paginatorQuery.SortDirection == "asc")
                    query = query.OrderBy(a => a.Rating).ToList();
                else
                    query = query.OrderByDescending(a => a.Rating).ToList();
            }
            return query;
        }

        public static List<Comment> SortByRating(this List<Comment> query, PaginatorQuery paginatorQuery)
        {
            if (!string.IsNullOrWhiteSpace(paginatorQuery.SortBy))
            {
                if (paginatorQuery.SortDirection == "asc")
                    query = query.OrderBy(c => c.Rating).ToList();
                else
                    query = query.OrderByDescending(c => c.Rating).ToList();
            }
            return query;
        }

        public static List<Article> RatingFilter(this List<Article> query, RequestFilters filters)
        {
            if (filters != null)
            {
                foreach (var filter in filters.Filters)
                {
                    if (filter.Path == "rating")
                    {
                        if (filter.Action == FiltersCompareActions.GreaterThenOrEqual)
                            query = query.Where(a => a.Rating >= int.Parse(filter.Value)).ToList();
                        else
                            query = query.Where(a => a.Rating >= int.Parse(filter.Value)).ToList();
                    }
                }
            }
            return query;
        }
    }
}
