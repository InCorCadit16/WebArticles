using AutoMapper;
using DataModel.Data.Entities;
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

namespace WebAPI.Infrastructure
{
    public static class QueryableExtensions
    {

        public static IQueryable<T> ApplyFilters<T>(this IQueryable<T> query, RequestFilters filters)
        {
            if (filters != null)
            {
                var predicate = new StringBuilder();
                for (int i = 0; i < filters.Filters.Count; i++)
                {
                    if (i > 0)
                    {
                        predicate.Append($" {filters.LogicalOperator} ");
                    }
                    predicate.Append(filters.Filters[i].Path + $" {filters.Filters[i].Action} (@{i})");
                }

                if (filters.Filters.Any())
                {
                    var propertyValues = filters.Filters.Select(f => f.Value).ToArray();

                    query = query.Where(predicate.ToString(), propertyValues);
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
            if (!string.IsNullOrWhiteSpace(paginatorQuery.SortBy))
            {
                query = query.OrderBy(paginatorQuery.SortBy + " " + paginatorQuery.SortDirection);
            }
            return query;
        }

        public static IQueryable<Article> TagsMatch(this IQueryable<Article> query, PaginatorQuery paginatorQuery)
        {
            if (paginatorQuery.Filters != null && paginatorQuery.Filters.Tags != null)
            {
                if (paginatorQuery.Filters.Tags.Count() > 0)
                {
                    query = query.Where(a => a.Tags.Split('#', StringSplitOptions.None).AsQueryable().Intersect(paginatorQuery.Filters.Tags).Any());
                }
            }
            return query;

        }

        public static async Task<TDestination[]> MapWithAsync<TSource, TDestination>(this IQueryable<TSource> query, IMapper mapper)
        {
            return mapper.Map<TDestination[]>(await query.ToArrayAsync());
        }

        
    }
}
