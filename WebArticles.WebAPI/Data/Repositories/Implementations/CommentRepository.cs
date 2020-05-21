using AutoMapper;
using DataModel.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    public class CommentRepository : EntityRepository<Comment>
    {

        public CommentRepository(ArticleDbContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<PaginatorAnswer<TDto>> GetUserCommentsPage<TDto>(long userId, PaginatorQuery paginatorQuery, params Expression<Func<Comment, object>>[] includeProperties) where TDto : class
        {
            IQueryable<Comment> query = IncludeProperties(includeProperties);

            query = query.Where(c => c.Reviewer.UserId == userId);

            var total = await query.CountAsync();

            query = query.GetPage(paginatorQuery.Page, paginatorQuery.PageSize);

            var result = _mapper.Map<TDto[]>(await query.ToArrayAsync());

            return new PaginatorAnswer<TDto>
            {
                Total = total,
                Items = result
            };
        }

        public async Task<PaginatorAnswer<TDto>> GetArticlesCommentsPage<TDto>(long articleId, PaginatorQuery paginatorQuery, params Expression<Func<Comment, object>>[] includeProperties) where TDto : class
        {
            IQueryable<Comment> query = IncludeProperties(includeProperties);

            query = query.Where(c => c.ArticleId == articleId);

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
