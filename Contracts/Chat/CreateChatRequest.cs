using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Contracts.Chat
{
    public class CreateChatRequest
    {
        public string ChatRoomName { get; set; } = null!;
    }
}