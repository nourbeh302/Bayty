using Microsoft.EntityFrameworkCore;
using Models.Entities;
using Models.IRepositories;

namespace EF_Modeling.Repositories
{
    public class MessageRepository : GenericRepository<Message, int>, IMessageRepository
    {
        private readonly AppDbContext _context;
        public MessageRepository(AppDbContext context) : base(context)
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
