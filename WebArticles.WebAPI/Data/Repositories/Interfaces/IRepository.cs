using DataModel.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebArticles.WebAPI.Infrastructure.Models;

namespace WebAPI.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity: class
    {
        Task<List<TEntity>> GetAll(params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> GetById(long id, params Expression<Func<TEntity, object>>[] includeProperties);

        Task<TEntity> Insert(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task<TEntity> Delete(long id);

        Task<PaginatorAnswer<TDto>> GetPage<TDto>(PaginatorQuery paginatorQuery, params Expression<Func<TEntity, object>>[] includeProperties)  where TDto: class;
    }
}
