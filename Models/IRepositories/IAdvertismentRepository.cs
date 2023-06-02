using Models.Entities;
namespace Models.IRepositories
{
    public interface IAdvertismentRepository : IGenericRepository<Advertisement, int>
    {

        //Task AddVillaAd(Villa villa, Advertisement ad);
        //Task<Villa> GetAdVilla(int id);
        //Task<IEnumerable<Villa>> GetAdVillaAll();
        //Task<Advertisement> GetAdProperty(int id);
        //Task<bool> RemoveAd(int id);
        //Task<Advertisement> GetAdBuilding(int id);

        Task<Advertisement> GetDetailedAd(string adId);
        Task AddAdvertisement(Advertisement ad);
        Task<IEnumerable<Advertisement>> GetTwentyAd(int pageSize, int pageNumber);

    }
}