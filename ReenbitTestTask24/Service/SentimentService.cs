using Azure;
using Azure.AI.TextAnalytics;
using ReenbitTestTask24.Entities;
using ReenbitTestTask24.Interfaces;

namespace ReenbitTestTask24.Service
{
    public class SentimentService : ISentimentService
    {
        private readonly TextAnalyticsClient _client;

        public SentimentService(string endpoint, string key)
        {
            var credentials = new AzureKeyCredential(key);
            _client = new TextAnalyticsClient(new Uri(endpoint), credentials);
        }

        public async Task<Sentiment> AnalyzeSentimentAsync(string text)
        {
            var response = await _client.AnalyzeSentimentAsync(text);
            var sentiment = response.Value;

            return new Sentiment
            {
                SentimentType = MapSentiment(sentiment.ConfidenceScores),
                PositiveScore = sentiment.ConfidenceScores.Positive,
                NegativeScore = sentiment.ConfidenceScores.Negative,
                NeutralScore = sentiment.ConfidenceScores.Neutral
            };
        }

        private SentimentType MapSentiment(SentimentConfidenceScores scores)
        {
            if (scores.Positive > scores.Negative && scores.Positive > scores.Neutral)
                return SentimentType.POSITIVE;
            if (scores.Negative > scores.Positive && scores.Negative > scores.Neutral)
                return SentimentType.NEGATIVE;
            return SentimentType.NEUTRAL;
        }
    }
}
