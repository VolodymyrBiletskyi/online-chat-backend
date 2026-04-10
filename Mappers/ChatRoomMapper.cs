using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.Chat;
using online_chat.DTOs;
using online_chat.Models;

namespace online_chat.Mappers
{
    public static class ChatRoomMapper
    {
        public static ChatRoomDto ToDto(this ChatRoom chatRoom)
        {
            return new ChatRoomDto
            {
                ChatRoomId = chatRoom.ChatRoomId,
                ChatRoomName = chatRoom.ChatName,
                CreatedAt = chatRoom.CreatedAt
            };
        }

        public static ChatRoom ToEntity(this CreateChatRequest request)
        {
            return new ChatRoom
            {
                ChatName = request.ChatRoomName
            };
        }
    }
}