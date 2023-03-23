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
        IGenericRepository<Property, int> Properties { get; }
        IGenericRepository<Villa, int> Villas { get; }
        IGenericRepository<Building, int> Buildings { get; }
        IGenericRepository<RefreshToken, int> RefreshTokens { get; }
        int Complete();
    }
}
