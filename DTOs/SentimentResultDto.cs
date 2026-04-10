using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.DTOs
{
    public class SentimentResultDto
    {
        public string Sentiment { get; set; } = null!;
        public double PositiveScore { get; set; }
        public double NeutralScore { get; set; }
        public double NegativeScore { get; set; }
    }
}