using System.Linq.Expressions;

namespace PrecierosEC.Core.Interface
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);
        void UpdateRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);

        int Count();
        int Count(Expression<Func<TEntity, bool>> predicate);

        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string Foreign);
        TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingleOrDefaultInclude(Expression<Func<TEntity, bool>> predicate, string Foreign);
        TEntity Get(int id);
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(long id);
        Task<List<TEntity>> ListAsync();
        void AddAsync(TEntity entity);
        int Save();
        Task<int> SaveAsync();
    }
}
