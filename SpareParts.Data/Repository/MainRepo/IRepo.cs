using System.Linq.Expressions;

namespace SpareParts.Data
{
    public interface IRepo<T, in TId> where T : class
    {
        T Get(TId id);

        Task<T> GetAsync(TId id);

        IEnumerable<T> GetAll(string[]? include = null);

        IEnumerable<T> GetAllByDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

        Task<IEnumerable<T>> GetAllAsync(string[]? include = null);

        Task<IEnumerable<T>> GetAllByFilterAsync(Expression<Func<T, bool>> filter, string[]? include = null);

        T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string[]? include = null);

        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null, string[]? include = null);

        T Add(T entity);

        Task<T> AddAsync(T entity);

        IEnumerable<T> AddRange(IEnumerable<T> entities);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);

        T Update(T entity);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        int Count();

        int Count(Expression<Func<T, bool>> filter);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        T GetFirstOrDefaultForShopping(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true);
    }
}