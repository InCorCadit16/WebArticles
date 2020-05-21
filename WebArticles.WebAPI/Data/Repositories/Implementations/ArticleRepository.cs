using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI;
using WebAPI.Infrastructure;
using WebArticles.WebAPI.Infrastructure.Models;

namespace WebArticles.WebAPI.Data.Repositories.Implementations
{
    public class ArticleRepository : EntityRepository<Article>
    {
        public ArticleRepository(ArticleDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<PaginatorAnswer<TDto>> GetUserArticlesPage<TDto>(long userId, PaginatorQuery paginatorQuery, params Expression<Func<Article, object>>[] includeProperties)
        {
            IQueryable<Article> query = IncludeProperties(includeProperties);

            query = query.Where(a => a.Writer.UserId == userId);

            var total = await query.CountAsync();

            query = query.GetPage(paginatorQuery.Page, paginatorQuery.PageSize);

            var result = _mapper.Map<TDto[]>(await query.ToArrayAsync());

            return new PaginatorAnswer<TDto>
            {
                Total = total,
                Items = result
            };
        }

        public async override Task<PaginatorAnswer<TDto>> GetPage<TDto>(PaginatorQuery paginatorQuery, params Expression<Func<Article, object>>[] includeProperties) where TDto : class
        {
            IQueryable<Article> query = IncludeProperties(includeProperties);

            query = query.ApplyFilters(paginatorQuery.Filters);

            if (!string.IsNullOrWhiteSpace(paginatorQuery.SearchString))
                query = query.Where(a => a.Title.Contains(paginatorQuery.SearchString));

            query = query.TagsMatch(paginatorQuery);

            var total = await query.CountAsync();

            query = query.Sort(paginatorQuery);

            query = query.GetPage(paginatorQuery.Page, paginatorQuery.PageSize);

            var result = _mapper.Map<TDto[]>(await query.ToArrayAsync());

            return new PaginatorAnswer<TDto>
            {
                Total = total,
                Items = result
            };
        }
    }
}
