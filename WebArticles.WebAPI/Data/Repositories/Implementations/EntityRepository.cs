using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data.Entities;
using System.Collections.Generic;
using System;
using WebArticles.WebAPI.Infrastructure.Models;
using System.Linq.Expressions;
using AutoMapper;
using WebAPI.Data.Repositories.Interfaces;
using WebAPI.Infrastructure;
using WebAPI;
using WebArticles.WebAPI.Infrastructure.Exceptions;

namespace WebArticles.WebAPI.Data.Repositories.Implementations
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ArticleDbContext _context;
        protected readonly IMapper _mapper;

        public EntityRepository(ArticleDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(long id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException($"Cannot find object of type \"{typeof(TEntity)}\" with id {id}", $"Cannot delete {typeof(TEntity)}. Object is missing.");
            }

            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> GetById(long id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = IncludeProperties(includeProperties);
            return await query.FirstOrDefaultAsync(t => t.Id == id);
        }
        
        public virtual async Task<PaginatorAnswer<TDto>> GetPage<TDto>(PaginatorQuery paginatorQuery, params Expression<Func<TEntity, object>>[] includeProperties) where TDto: class
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            query = query.ApplyFilters(paginatorQuery.Filters);

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

        public async Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties)
        {

            IQueryable<TEntity> query = IncludeProperties(includeProperties);
            return await query.ToListAsync();
        }

        protected IQueryable<TEntity> IncludeProperties(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> entities = _context.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                if (includeProperty.Body.ToString().Contains("."))
                {
                    string body = includeProperty.Body.ToString();
                    string property = body.Substring(body.IndexOf(".") + 1);
                    entities = entities.Include(property);
                }
                else
                    entities = entities.Include(includeProperty);
            }
            return entities;
        }
    }
}
