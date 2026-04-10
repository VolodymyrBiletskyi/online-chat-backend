using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Contracts.Chat
{
    public class JoinChatRequest
    {
        public Guid UserId { get; set; }
        public Guid ChatRoomId { get; set; }
        public string UserName { get; set; } = null!;
        public string ChatName { get; set; } = null!;
    }
}