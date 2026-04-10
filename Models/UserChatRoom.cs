using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Models
{
    public class UserChatRoom
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
    }
}