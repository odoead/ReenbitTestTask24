using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReenbitTestTask24.Entities
{
    public class Sentiment
    {
        [Key]
        public int Id { get; set; }
        public SentimentType SentimentType { get; set; }
        public double PositiveScore { get; set; }
        public double NegativeScore { get; set; }
        public double NeutralScore { get; set; }

        [ForeignKey("ChatMessageId")]
        public ChatMessage ChatMessage { get; set; }
        public int ChatMessageId { get; set; }
    }
}
