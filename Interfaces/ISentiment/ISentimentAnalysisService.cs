using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.DTOs;

namespace online_chat.Interfaces.ISentiment
{
    public interface ISentimentAnalysisService
    {
        Task<SentimentResultDto> AnalyzeAsync(string text);
    }
}