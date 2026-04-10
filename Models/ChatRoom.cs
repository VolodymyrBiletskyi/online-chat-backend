using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Models
{
    public class ChatRoom
    {
        public Guid ChatRoomId { get; set; } = Guid.NewGuid();
        public string ChatName { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<UserChatRoom> UserChatRooms { get; set; } = new List<UserChatRoom>();
        public ICollection<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}