using System.Linq.Expressions;

namespace Villa_API.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll(Expression<Func<T, bool>>? filter = null);
        Task<T> Get(Expression<Func<T, bool>>? filter = null, bool tracked = true);
        Task Create(T entity);
        Task Delete(T entity);
        Task Save();
    }
}
