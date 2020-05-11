using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using DataModel.Data.Entities;

namespace WebAPI.Data.Repositories
{
    public class DbRepository : IRepository
    {
        private readonly ArticleDbContext _context;

        public DbRepository(ArticleDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll<T>() where T: class
        {
            IQueryable<T> query = _context.Set<T>();

            return query;
        }

        public void Delete<T>(T entity)
        {
            _context.Remove(entity);
        }

        public void Insert<T>(T entity)
        {
            _context.AddAsync(entity);
        }

        public void Update<T>(T entity)
        {
            _context.Update(entity);
        }

        public int SaveChanges()
        {
            return  _context.SaveChanges();
        }
    }
}
