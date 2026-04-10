using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.Chat;
using online_chat.DTOs;
using online_chat.Interfaces.IChat;
using online_chat.Interfaces.IUser;
using online_chat.Mappers;

namespace online_chat.Services
{
    public class ChatRoomService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepo;

        public ChatRoomService(IChatRepository chatRepository, IUserRepository userRepo)
        {
            _chatRepository = chatRepository;
            _userRepo = userRepo;

        }

        public async Task<ChatRoomDto> CreateChatRoomAsync(CreateChatRequest request)
        {
            var existingChat = await _chatRepository.GetChatByNameAsync(request.ChatRoomName);

            if (existingChat != null)
            {
                throw new InvalidOperationException("A chat room with this name already exists.");
            }

            var chatRoomEntity = request.ToEntity();

            await _chatRepository.AddChat(chatRoomEntity);
            await _chatRepository.SaveChangesAsync();

            return chatRoomEntity.ToDto();
        }

        public async Task<IReadOnlyList<ChatRoomDto>> GetAllChatRoomsAsync()
        {
            var chatRooms = await _chatRepository.GetAllChatRoomsAsync();
            return chatRooms.Select(ChatRoomMapper.ToDto).ToList();
        }
        public async Task<ChatRoomDto?> GetChatByNameAsync(string chatName)
        {
            if (string.IsNullOrWhiteSpace(chatName))
                throw new ArgumentException("Chat name cannot be empty.");

            var chat = await _chatRepository.GetChatByNameAsync(chatName.Trim());

            return chat is null ? null : ChatRoomMapper.ToDto(chat);
        }

        public async Task<IReadOnlyList<ChatRoomDto>> GetChatsByUserAsync(Guid userId)
        {
            var userExists = await _userRepo.GetByIdAsync(userId);

            if (userExists is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var chats = await _chatRepository.GetChatsByUser(userId);

            return chats.Select(ChatRoomMapper.ToDto).ToList();
        }

        public async Task AddUserToChatAsync(Guid userId, Guid chatRoomId)
        {
            var userExists = await _userRepo.GetByIdAsync(userId);

            if (userExists is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            var chatExists = await _chatRepository.GetChatByIdAsync(chatRoomId);

            if (chatExists is null)
            {
                throw new KeyNotFoundException("Chat room not found");
            }

            var alreadyInChat = await _chatRepository.IsUserInChatAsync(userId, chatRoomId);

            if (alreadyInChat)
            {
                return;
            }

            await _chatRepository.AddUserToChatAsync(userId, chatRoomId);
            await _chatRepository.SaveChangesAsync();
        }

    }
}