using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.Message;
using online_chat.DTOs;
using online_chat.Models;

namespace online_chat.Mappers
{
    public static class MessageMapper
    {
        public static MessageDto ToMessageDto(this ChatMessage message)
        {
            return new MessageDto
            {
                MessageId = message.ChatMessageId,
                ChatRoomId = message.ChatRoomId,
                UserId = message.UserId,
                Username = message.User.Username,
                Text = message.Text,
                Sentiment = message.Sentiment,
                PositiveScore = message.PositiveScore,
                NeutralScore = message.NeutralScore,
                NegativeScore = message.NegativeScore,
                SentAt = message.SentAt
            };
        }

        public static ChatMessage ToEntity(SaveMessageRequest request)
        {
            return new ChatMessage
            {
                ChatMessageId = Guid.NewGuid(),
                ChatRoomId = request.ChatRoomId,
                UserId = request.UserId,
                Text = request.Text,
                SentAt = DateTime.UtcNow
            };
        }
    }
}