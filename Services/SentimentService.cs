using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.AI.TextAnalytics;
using online_chat.DTOs;
using online_chat.Interfaces.ISentiment;
using online_chat.Mappers;

namespace online_chat.Services
{
  public class SentimentService : ISentimentAnalysisService
  {
    private readonly TextAnalyticsClient _textClient;

    public SentimentService(IConfiguration configuration)
    {
      var endpoint = configuration["AzureTextAnalytics:Endpoint"];
      var apiKey = configuration["AzureTextAnalytics:ApiKey"];

      if (string.IsNullOrWhiteSpace(endpoint))
        throw new Exception("AzureTextAnalytics:Endpoint is missing");

      if (string.IsNullOrWhiteSpace(apiKey))
        throw new Exception("AzureTextAnalytics:ApiKey is missing");

      _textClient = new TextAnalyticsClient(
          new Uri(endpoint),
          new AzureKeyCredential(apiKey)
      );
    }
    public async Task<SentimentResultDto> AnalyzeAsync(string text)
    {
      if (string.IsNullOrWhiteSpace(text))
      {
        return new SentimentResultDto
        {
          Sentiment = "neutral",
          PositiveScore = 0,
          NeutralScore = 1,
          NegativeScore = 0
        };
      }

      var response = await _textClient.AnalyzeSentimentAsync(text);

      return response.Value.ToSentimentResultDto();
    }
  }
}