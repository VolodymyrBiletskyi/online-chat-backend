using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.DTOs
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public Guid ChatRoomId { get; set; }

        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;

        public string Text { get; set; } = null!;
        public string? Sentiment { get; set; }
        public double? PositiveScore { get; set; }
        public double? NeutralScore { get; set; }
        public double? NegativeScore { get; set; }
        public DateTime SentAt { get; set; }
    }
}