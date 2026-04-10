using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Contracts.Message
{
    public class SaveMessageRequest
    {
        public Guid ChatRoomId { get; set; }
        public Guid UserId { get; set; }
        public string Text { get; set; } = null!;
    }
}