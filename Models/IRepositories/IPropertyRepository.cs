using Models.Entities;

namespace Models.IRepositories
{
    public interface IPropertyRepository
    {
        Task<IEnumerable<Apartment>> Pagination(int pageNumber, int pageSize);
        Task<IEnumerable<Apartment>> ApartmentsOfEnterprise(int enterpriseId);
    }
}
