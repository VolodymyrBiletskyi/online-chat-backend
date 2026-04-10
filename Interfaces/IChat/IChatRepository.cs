using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Models;

namespace online_chat.Interfaces.IChat
{
    public interface IChatRepository
    {
        Task AddChat(ChatRoom chatRoom);
        Task<ChatRoom?> GetChatByNameAsync(string chatName);
        Task<int> SaveChangesAsync();
        Task<IReadOnlyList<ChatRoom>> GetAllChatRoomsAsync();
        Task<ChatRoom?> GetChatByIdAsync(Guid chatRoomId);
        Task<IReadOnlyList<ChatRoom>> GetChatsByUser(Guid userId);
        Task<bool> IsUserInChatAsync(Guid userId, Guid chatRoomId);
        Task AddUserToChatAsync(Guid userId, Guid chatRoomId);
    }
}