using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.AI.TextAnalytics;
using online_chat.DTOs;

namespace online_chat.Mappers
{
    public static class SentimentMapper
    {
        public static SentimentResultDto ToSentimentResultDto(this DocumentSentiment sentiment)
        {
            return new SentimentResultDto
            {
                Sentiment = sentiment.Sentiment.ToString().ToLower(),
                PositiveScore = sentiment.ConfidenceScores.Positive,
                NeutralScore = sentiment.ConfidenceScores.Neutral,
                NegativeScore = sentiment.ConfidenceScores.Negative
            };
        }
    }
}