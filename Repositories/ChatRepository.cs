using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using online_chat.Data;
using online_chat.Interfaces.IChat;
using online_chat.Models;

namespace online_chat.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _dbContext;
        public ChatRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task AddChat(ChatRoom chatRoom)
        {
            return _dbContext.ChatRooms.AddAsync(chatRoom).AsTask();
        }

        public async Task<IReadOnlyList<ChatRoom>> GetAllChatRoomsAsync()
        {
            return await _dbContext.ChatRooms
            .AsNoTracking()
            .ToListAsync();
        }

        public Task<ChatRoom?> GetChatByIdAsync(Guid chatRoomId)
        {
            return _dbContext.ChatRooms.FirstOrDefaultAsync(c => c.ChatRoomId == chatRoomId);
        }

        public async Task<ChatRoom?> GetChatByNameAsync(string chatName)
        {
            return await _dbContext.ChatRooms.FirstOrDefaultAsync(n => n.ChatName == chatName);
        }

        public async Task<IReadOnlyList<ChatRoom>> GetChatsByUser(Guid userId)
        {
            var chats = await _dbContext.ChatRooms
            .Where(chat => chat.UserChatRooms.Any(x => x.UserId == userId))
            .ToListAsync();
            return chats;
        }

        public async Task<bool> IsUserInChatAsync(Guid userId, Guid chatRoomId)
        {
            return await _dbContext.UserChatRooms
                .AnyAsync(x => x.UserId == userId && x.ChatRoomId == chatRoomId);
        }

        public async Task AddUserToChatAsync(Guid userId, Guid chatRoomId)
        {
            var link = new UserChatRoom
            {
                UserId = userId,
                ChatRoomId = chatRoomId
            };

            await _dbContext.UserChatRooms.AddAsync(link);
        }

        public Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}