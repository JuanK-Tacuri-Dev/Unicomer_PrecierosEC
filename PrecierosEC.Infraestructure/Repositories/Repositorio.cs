using Microsoft.EntityFrameworkCore;
using PrecierosEC.Core.Interface;
using PrecierosEC.Infraestructure.Data;
using System.Linq.Expressions;

namespace PrecierosEC.Infraestructure
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        protected Context _context;
        protected readonly DbSet<TEntity> _entities;


        public Repositorio(Context contx)
        {
            _context = new Context();
            _entities = _context.Set<TEntity>();
        }


        public virtual void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            _entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            _entities.UpdateRange(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }

        public virtual int Count()
        {
            return _entities.Count();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Count(predicate);
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string Foreign)
        {
            return _entities.Where(predicate).Include(Foreign);
        }

        public virtual TEntity GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).FirstOrDefault();
        }
        public virtual TEntity GetSingleOrDefaultInclude(Expression<Func<TEntity, bool>> predicate, string Foreign)
        {
            return _entities.Where(predicate).Include(Foreign).FirstOrDefault();
        }

        public virtual TEntity Get(int id)
        {
            return _entities.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _entities.FindAsync(id);
        }

        public async Task<List<TEntity>> ListAsync()
        {
            return await _entities.ToListAsync();
        }

        public async void AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        [Obsolete]
        public TEntity Get(long id)
        {
            return _entities.Find(id);
        }

        [Obsolete]
        public IEnumerable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).AsNoTracking().ToList();
        }

        public int Save()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch (ObjectDisposedException)
            {
                _context = new Context();
                return _context.SaveChanges();
            }

        }

        public async Task<int> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (ObjectDisposedException)
            {
                _context = new Context();
                return await _context.SaveChangesAsync();
            }


        }
    }
}
