using System.ComponentModel.DataAnnotations;

namespace ReenbitTestTask24.Entities
{
    public class ChatMessage
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public Sentiment Sentiment { get; set; }
    }

    public enum SentimentType
    {
        NEUTRAL = 0,
        POSITIVE,
        NEGATIVE
    }
}
