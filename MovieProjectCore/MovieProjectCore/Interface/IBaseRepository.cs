using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MovieProjectCore.Interface
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        Task AddAsync(IEnumerable<TEntity> entities);

        Task<TEntity> GetAsync(Guid id);

        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(int pageNumber, int pageSize);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression);

        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, int pageNumber, int pageSize);

        void Remove(TEntity entity);

        void Remove(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);
    }
}
