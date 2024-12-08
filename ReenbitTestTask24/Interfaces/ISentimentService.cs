using ReenbitTestTask24.Entities;

namespace ReenbitTestTask24.Interfaces
{
    public interface ISentimentService
    {
        public Task<Sentiment> AnalyzeSentimentAsync(string text);

    }
}
