using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.IRepositories;

namespace EFCoreModelBuilder.Repositories
{
    public class MessageRepository : GenericRepository<Message, int>, IMessageRepository
    {
        private readonly DataContext _context;
        public MessageRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Message>> RetrieveMessageForTwoUsers(string firstUserId, string secondUserId)
            => await _context.Set<Message>()
            .Where(m => (m.SenderId == firstUserId  && m.ReceiverId == secondUserId) ||
                        (m.SenderId == secondUserId && m.ReceiverId == firstUserId))
            .ToListAsync();
    }
}
