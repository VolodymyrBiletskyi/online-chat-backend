using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Models
{
    public class ChatMessage
    {
        public Guid ChatMessageId { get; set; } = Guid.NewGuid();

        public Guid ChatRoomId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string Text { get; set; } = null!;
        public string? Sentiment { get; set; }
        public double? PositiveScore { get; set; }
        public double? NeutralScore { get; set; }
        public double? NegativeScore { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}