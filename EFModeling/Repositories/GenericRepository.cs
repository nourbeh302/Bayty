using Microsoft.EntityFrameworkCore;
using Models.IRepositories;
using System.Linq;
using System.Linq.Expressions;

namespace EF_Modeling.Repositories
{
    public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T item)
        {
            await _context.Set<T>().AddAsync(item);
        }

        public async Task AddRangeAsync(IEnumerable<T> items) => await _context.Set<T>().AddRangeAsync(items);

        public T Delete(K id)
        {
            var result = _context.Set<T>().Find(id);
            if (result == null)
            {
                return null;
            }
            _context.Remove(result);
            return result;
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria)
                    => await _context.Set<T>().Where(criteria).ToListAsync();

        public async Task<T> FindOneAsync(Expression<Func<T, bool>> criteria)
                    => await _context.Set<T>().FirstOrDefaultAsync(criteria);

        public async Task<IEnumerable<T>> GetAllAsync()
                    => await _context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(K id)
                    => await _context.Set<T>().FindAsync(id);

        public async Task UpdateAsync(K id, T item)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result != null)
                _context.Entry(item).State = EntityState.Modified;
        }
    }
}
