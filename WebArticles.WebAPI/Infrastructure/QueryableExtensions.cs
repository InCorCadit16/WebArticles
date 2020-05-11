using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebArticles.WebAPI.Data.Dto;

namespace WebAPI.Infrastructure
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> GetPage<T>(this IQueryable<T> query, int p, int size)
        {
            return query.Skip((p - 1) * size).Take(size);
        }

        public static async Task<TDestination[]> MapWithAsync<TDestination, TSource>(this IQueryable<TSource> query, IMapper mapper)
        {
            return mapper.Map<TDestination[]>(await query.ToArrayAsync());
        }

        public static async Task<Article[]> FilterAsync(this IQueryable<Article> query, Filters filters)
        {
            if (filters.Topics != null)
            {
                query = query.Where(a => filters.Topics.Contains(a.Topic.TopicName));
            }

            if (filters.MinRating.HasValue)
            {
                query = query.Where(a => a.Rating >= filters.MinRating);
            }

            if (filters.MaxRating.HasValue)
            {
                query = query.Where(a => a.Rating <= filters.MaxRating);
            }

            if (filters.MinDate.HasValue)
            {
                query = query.Where(a => a.PublichDate >= filters.MinDate);
            }

            if (filters.MaxDate.HasValue)
            {
                query = query.Where(a => a.PublichDate <= filters.MaxDate);
            }

            var result = await query.ToArrayAsync();

            if (filters.Tags != null)
            {
                var tags = filters.Tags.Substring(1).Split('#');
                result = result.Where(a => tags.Any(t => a.Tags.Contains("#" + t))).ToArray();
            }

            return result;
        }
    }
}
