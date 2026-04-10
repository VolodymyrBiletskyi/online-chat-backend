using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.DTOs
{
    public class ChatRoomDto
    {
        public Guid ChatRoomId { get; set; }
        public string ChatRoomName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}