using Microsoft.EntityFrameworkCore;
using Models.IRepositories;
using System.Linq;
using System.Linq.Expressions;

namespace EFCoreModelBuilder.Repositories
{
    public class GenericRepository<T, K> : IGenericRepository<T, K> where T : class
    {
        private readonly DataContext _context;
        public GenericRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T item) => await _context.Set<T>().AddAsync(item);

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

        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria)
                    => await _context.Set<T>().Where(criteria).ToListAsync();

        public async Task<T> FindByIdAsnyc(K id) => await _context.Set<T>().FindAsync(id);
        public async Task<T> FindOneAsync(Expression<Func<T, bool>> criteria)
                    => await _context.Set<T>().FirstOrDefaultAsync(criteria);

        public async Task<IEnumerable<T>> GetAllAsync()
                    => await _context.Set<T>().ToListAsync();

        public async Task<T> GetAsync(K id)
                    => await _context.Set<T>().FindAsync(id);

        public int GetCount() => _context.Set<T>().Count();


        public async Task UpdateAsync(K id, T item)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result != null)
                _context.Entry(item).State = EntityState.Modified;
        }
    }
}
