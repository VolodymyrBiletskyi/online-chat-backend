using online_chat.Models;

namespace online_chat.Interfaces.IMessage
{
    public interface IMessageRepository
    {
        Task<IReadOnlyList<ChatMessage>> GetMessagesByChatRoomIdAsync(Guid chatRoomId);
        Task AddMessage(ChatMessage message);
        Task<int> SaveChangesAsync();
    }
}