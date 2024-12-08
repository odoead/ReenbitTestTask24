using Microsoft.EntityFrameworkCore;
using ReenbitTestTask24.DB;
using ReenbitTestTask24.DTO;
using ReenbitTestTask24.Interfaces;

namespace ReenbitTestTask24.Service
{
    public class MessageService : IMessageService
    {
        private readonly Context dbcontext;
        public MessageService(Context context)
        {
            dbcontext = context;
        }
        public async Task<List<MessageDTO>> GetMessages()
        {
            return await dbcontext.ChatMessages
                 .Include(m => m.Sentiment)
                 .OrderByDescending(m => m.Timestamp)
                 .Select(q => new MessageDTO
                 {
                     Message = q.Message,
                     NegativeScore = q.Sentiment.NegativeScore,
                     Sentiment = (int)q.Sentiment.SentimentType,
                     NeutralScore = q.Sentiment.NeutralScore,
                     PositiveScore = q.Sentiment.PositiveScore,
                     Timestamp = q.Timestamp,
                     UserId = q.UserId,
                     Username = q.Username,
                 })
                 .ToListAsync();
        }
    }
}
