using EFModeling.Repositories;
using Models.DataStoreContract;
using Models.Entities;
using Models.IRepositories;

namespace EFModeling.DataStore
{
    public class DataStore : IDataStore
    {
        private readonly ApplicationDbContext _context;
        public DataStore(ApplicationDbContext context)
        {
            _context = context;
            Users = new GenericRepository<User, string>(context);
            Cards = new GenericRepository<Card, int>(context);
            RefreshTokens = new GenericRepository<RefreshToken, int>(context);
            Messages = new MessageRepository(context);
        }

        public IGenericRepository<User, string> Users { get; private set; }

        public IGenericRepository<Card, int> Cards { get; private set; }

        public IGenericRepository<Enterprise, int> Enterprises { get; private set; }

        public IGenericRepository<Report, int> Reports { get; private set; }

        public IGenericRepository<Message, int> Messages { get; private set; }

        public IGenericRepository<RefreshToken, int> RefreshTokens { get; private set; }
        
        public IGenericRepository<Property, int> Properties { get; private set; }

        public IGenericRepository<Villa, int> Villas { get; private set; }

        public IGenericRepository<Building, int> Buildings { get; private set; }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
