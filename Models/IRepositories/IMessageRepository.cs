using Models.Entities;

namespace Models.IRepositories
{
    public interface IMessageRepository : IGenericRepository<Message, int>
    {
        Task<IEnumerable<Message>> RetrieveMessageForTwoUsers(string firstUserId, string secondUserId);
    }
}
