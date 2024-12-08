using ReenbitTestTask24.Entities;

namespace ReenbitTestTask24.DTO
{
    public class MessageDTO
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public int Sentiment { get; set; }
        public double PositiveScore { get; set; }
        public double NegativeScore { get; set; }
        public double NeutralScore { get; set; }
    }
}
