﻿using Microsoft.AspNetCore.SignalR;
using ReenbitTestTask24.DB;
using ReenbitTestTask24.DTO;
using ReenbitTestTask24.Entities;
using ReenbitTestTask24.Interfaces;
using E = Microsoft.AspNetCore.SignalR;

namespace ReenbitTestTask24.Hub
{
    public class MessageHub : E.Hub
    {
        private readonly Context dbcontext;
        private readonly ISentimentService sentimentService;
        public MessageHub(Context context, ISentimentService sentimentService)
        {
            dbcontext = context;
            this.sentimentService = sentimentService;
        }

        public async Task Send(string username, string message)
        {
            var chatMessage = new ChatMessage
            {
                UserId = Context.ConnectionId,
                Username = username,
                Message = message,
                Timestamp = DateTime.UtcNow,
            };

            if (sentimentService != null)
            {
                var sentimentResult = await sentimentService.AnalyzeSentimentAsync(message);
                chatMessage.Sentiment = sentimentResult;

                sentimentResult.ChatMessageId = chatMessage.Id;
                dbcontext.Sentiments.Add(sentimentResult);
            }

            dbcontext.ChatMessages.Add(chatMessage);
            await dbcontext.SaveChangesAsync();

            var sentiment = dbcontext.Sentiments.Where(s => s.ChatMessageId == chatMessage.Id).FirstOrDefault();

            var outputMessage = new MessageDTO
            {
                Message = chatMessage.Message,
                Username = chatMessage.Username,
                Timestamp = chatMessage.Timestamp,
                UserId = chatMessage.UserId,
                NegativeScore = sentiment.NegativeScore ,  
                NeutralScore = sentiment.NeutralScore,
                PositiveScore = sentiment.PositiveScore ,
                Sentiment = (int)sentiment.SentimentType   
            };
            await Clients.All.SendAsync("Send", outputMessage);
        }
    }
}
