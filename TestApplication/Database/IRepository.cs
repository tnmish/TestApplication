using System.Linq.Expressions;

namespace TestApplication.Database
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task CreateAsync(TEntity item);
        Task<TEntity?> FindByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAsync();
        Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task RemoveAsync(TEntity item);
        Task UpdateAsync(TEntity item);
    }
}
