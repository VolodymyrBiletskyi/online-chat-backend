using online_chat.Data;
using online_chat.Interfaces.IMessage;
using online_chat.Models;
using Microsoft.EntityFrameworkCore;

namespace online_chat.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _dbContext;

        public MessageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddMessage(ChatMessage message)
        {
            return _dbContext.ChatMessages.AddAsync(message).AsTask();
        }

        public async Task<IReadOnlyList<ChatMessage>> GetMessagesByChatRoomIdAsync(Guid chatRoomId)
        {
            return await _dbContext.ChatMessages
                .Include(x => x.User)
                .Where(m => m.ChatRoomId == chatRoomId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}