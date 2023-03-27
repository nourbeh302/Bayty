using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.IRepositories;

namespace EFModeling.Repositories
{
    public class PropertyRepository : GenericRepository<Property, int>, IPropertyRepository
    {
        private readonly ApplicationDbContext _context;
        public PropertyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Apartment>> Pagination(int pageNumber, int pageSize)
            => await _context.Set<Apartment>().Skip((pageSize -1) * pageNumber).Take(pageSize).ToListAsync();

        public async Task<IEnumerable<Apartment>> ApartmentsOfEnterprise(int enterpriseId)
        {
            throw new NotImplementedException();
        }
    }
}
