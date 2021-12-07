using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Example.Library.DataAccessSQL.Repositories
{
    public interface IRepository<TEntity, TIdentifier>
    {
        Task<TEntity> GetByIdAsync(TIdentifier id);
        Task<IEnumerable<TEntity>> GetItemsAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filters);
    }
}
