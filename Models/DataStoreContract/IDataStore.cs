using Models.Entities;
using Models.IRepositories;
namespace Models.DataStoreContract
{
    public interface IDataStore : IDisposable
    {
        IGenericRepository<User, string> Users { get; }
        IGenericRepository<Card, int> Cards { get; }
        IGenericRepository<Enterprise, int> Enterprises { get; }
        IGenericRepository<Report, int> Reports { get; }
        IGenericRepository<Message, int> Messages { get; }
        IGenericRepository<Apartment, int> Apartments { get; }
        IGenericRepository<FavoriteProperties, int> FavoriteProperties { get; }
        IGenericRepository<Villa, int> Villas { get; }
        IGenericRepository<Building, int> Buildings { get; }
        IAdvertismentRepository Advertisements { get; }
        IGenericRepository<RefreshToken, int> RefreshTokens { get; }
        int Complete();
        Task<int> CompleteAsync();

    }
}
