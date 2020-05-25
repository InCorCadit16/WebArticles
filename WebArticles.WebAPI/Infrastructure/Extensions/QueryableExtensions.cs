using AutoMapper;
using WebArticles.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dtos;
using WebArticles.WebAPI.Infrastructure.Models;
using System.Collections.Generic;

namespace WebAPI.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, RequestFilters filters)
        {
            if (filters != null)
            {
                if (filters.Filters.Any())
                {
                    var predicate = new StringBuilder();
                    var propertyValues = filters.Filters.Select(f => f.Value).ToArray();
                    for (int i = 0; i < filters.Filters.Count; i++)
                    {
                        
                        if (filters.Filters[i].Path == "rating") continue;

                        if (i > 0)
                        {
                            predicate.Append($" {filters.LogicalOperator} ");
                        }
                        predicate.Append($"{filters.Filters[i].Path} {filters.Filters[i].Action} ({propertyValues[i]})");
                    }

                    query = query.Where(predicate.ToString());
                }
                
            }

            return query;
        }

        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int p, int size)
        {
            return query.Skip((p - 1) * size).Take(size);
        }

        public static IQueryable<T> Sort<T>(this IQueryable<T> query, PaginatorQuery paginatorQuery)
        {
            if (!string.IsNullOrWhiteSpace(paginatorQuery.SortBy) && paginatorQuery.SortBy != "rating")
            {
                query = query.OrderBy(paginatorQuery.SortBy + " " + paginatorQuery.SortDirection);
            }
            return query;
        }

        

        public static async Task<TDestination[]> MapWithAsync<TSource, TDestination>(this IQueryable<TSource> query, IMapper mapper)
        {
            return mapper.Map<TDestination[]>(await query.ToArrayAsync());
        }
    }
}
