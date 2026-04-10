using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.Message;
using online_chat.DTOs;
using online_chat.Interfaces.IChat;
using online_chat.Interfaces.IMessage;
using online_chat.Interfaces.ISentiment;
using online_chat.Interfaces.IUser;
using online_chat.Mappers;

namespace online_chat.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepo;
        private readonly IUserRepository _userRepo;
        private readonly IChatRepository _chatRoomRepo;
        private readonly ISentimentAnalysisService _sentimentService;

        public MessageService(IMessageRepository messageRepo,
         IUserRepository userRepo,
          IChatRepository chatRoomRepo,
          ISentimentAnalysisService sentimentService)
        {
            _messageRepo = messageRepo;
            _userRepo = userRepo;
            _chatRoomRepo = chatRoomRepo;
            _sentimentService = sentimentService;
        }

        public async Task<MessageDto> AddMessageAsync(SaveMessageRequest request)
        {
            var chatRoomExists = await _chatRoomRepo.GetChatByIdAsync(request.ChatRoomId);

            if (chatRoomExists == null)
            {
                throw new Exception("Chat room not found");
            }

            var userExists = await _userRepo.GetByIdAsync(request.UserId);

            if (userExists == null)
            {
                throw new Exception("User not found");
            }

            var message = MessageMapper.ToEntity(request);

            var sentiment = await _sentimentService.AnalyzeAsync(message.Text);

            message.Sentiment = sentiment.Sentiment;
            message.PositiveScore = sentiment.PositiveScore;
            message.NeutralScore = sentiment.NeutralScore;
            message.NegativeScore = sentiment.NegativeScore;

            await _messageRepo.AddMessage(message);
            await _messageRepo.SaveChangesAsync();

            return new MessageDto
            {
                MessageId = message.ChatMessageId,
                ChatRoomId = message.ChatRoomId,
                UserId = message.UserId,
                Username = userExists.Username,
                Text = message.Text,
                SentAt = message.SentAt,
                Sentiment = message.Sentiment,
                PositiveScore = message.PositiveScore,
                NeutralScore = message.NeutralScore,
                NegativeScore = message.NegativeScore
            };
        }

        public async Task<IReadOnlyList<MessageDto>> GetMessagesByChatRoomIdAsync(Guid chatRoomId)
        {
            var messages = await _messageRepo.GetMessagesByChatRoomIdAsync(chatRoomId);

            return messages.Select(MessageMapper.ToMessageDto).ToList();
        }
    }
}