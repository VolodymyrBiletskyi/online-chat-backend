using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.Chat;
using online_chat.DTOs;

namespace online_chat.Interfaces.IChat
{
    public interface IChatService
    {
        Task<ChatRoomDto> CreateChatRoomAsync(CreateChatRequest request);
        Task<IReadOnlyList<ChatRoomDto>> GetAllChatRoomsAsync();
        Task<IReadOnlyList<ChatRoomDto>> GetChatsByUserAsync(Guid userId);
        Task<ChatRoomDto?> GetChatByNameAsync(string chatName);
        Task AddUserToChatAsync(Guid userId, Guid chatRoomId);

    }
}