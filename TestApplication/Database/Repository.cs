using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TestApplication.Database
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;
        public DbSet<TEntity> _dbSet;
        public Repository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }
                
        public async Task<TEntity?> FindByIdAsync(int id)
        {            
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.Where(predicate).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TEntity item)
        {
            _dbSet.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task RemoveAsync(TEntity item)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}
