using AutoMapper;
using WebArticles.DataModel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebAPI;
using WebAPI.Data.Repositories.Interfaces;
using WebArticles.WebAPI.Infrastructure.Exceptions;
using WebArticles.WebAPI.Infrastructure.Models;
using WebAPI.Infrastructure.Extensions;

namespace WebArticles.WebAPI.Data.Repositories.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private readonly ArticleDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(ArticleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<User> Delete(long id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Cannot find object of type \"{typeof(User)}\" with id {id}", $"Cannot delete {typeof(User)}. Object is missing.");
            }

            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<User> Insert(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByUserName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == name);
        }

        public async Task<User> GetUserByEmail(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == name);
        }

        public async Task<User> GetUserWithAllProperties(long id)
        {
            IQueryable<User> query = _context.Users;

            query = query.Include(u => u.Writer)
                            .ThenInclude(w => w.TopicsLink)
                                .ThenInclude(wt => wt.Topic)
                          .Include(u => u.Writer)
                            .ThenInclude(w => w.Articles)
                                .ThenInclude(a => a.UserArticleMarks);

            query = query.Include(u => u.Reviewer)
                            .ThenInclude(r => r.TopicsLink)
                                .ThenInclude(rt => rt.Topic)
                          .Include(u => u.Reviewer)
                            .ThenInclude(w => w.Comments)
                                .ThenInclude(a => a.UserCommentMarks);

            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetById(long id, params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<PaginatorAnswer<TDto>> GetPage<TDto>(PaginatorQuery paginatorQuery, params Expression<Func<User, object>>[] includeProperties) where TDto : class
        {
            IQueryable<User> query = IncludeProperties(includeProperties);
            if (includeProperties == null)
                query = _context.Users.Include(u => u.Writer).ThenInclude(w => w.Articles).Include(u => u.Reviewer).ThenInclude(r => r.Comments);

            var total = await query.CountAsync();

            query = query.Sort(paginatorQuery);

            query = query.GetPage(paginatorQuery.Page, paginatorQuery.PageSize);

            var result =  _mapper.Map<TDto[]>(await query.ToArrayAsync());

            return new PaginatorAnswer<TDto>
            {
                Total = total,
                Items = result
            };
        }

        public async Task<List<User>> GetAll(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> query = IncludeProperties(includeProperties);
            return await query.ToListAsync();
        }

        public async Task SaveAllChanges()
        {
            await _context.SaveChangesAsync();
        }

        private IQueryable<User> IncludeProperties(params Expression<Func<User, object>>[] includeProperties)
        {
            IQueryable<User> entities = _context.Users;
            foreach (var includeProperty in includeProperties)
            {
                entities = entities.Include(includeProperty);
            }
            return entities;
        }

      
    }
}
