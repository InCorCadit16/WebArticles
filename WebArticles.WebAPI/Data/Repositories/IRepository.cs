using System.Linq;

namespace WebAPI.Data.Repositories
{
    public interface IRepository
    {
        IQueryable<T> GetAll<T>() where T: class;

        void Insert<T>(T entity);

        void Update<T>(T entity);

        void Delete<T>(T entity);

        int SaveChanges();
    }
}
