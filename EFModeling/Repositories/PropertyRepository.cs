using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.IRepositories;

namespace EF_Modeling.Repositories
{
    public class PropertyRepository : GenericRepository<Property, int>, IPropertyRepository
    {
        private readonly AppDbContext _context;
        public PropertyRepository(AppDbContext context) : base(context)
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
