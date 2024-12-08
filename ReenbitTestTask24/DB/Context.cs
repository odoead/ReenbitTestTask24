using Microsoft.EntityFrameworkCore;
using ReenbitTestTask24.Entities;
using System.Reflection;

namespace ReenbitTestTask24.DB
{
    public class Context : DbContext
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<Sentiment> Sentiments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            

            builder.Entity<Sentiment>()
            .Property(e => e.SentimentType)
            .HasConversion<int>();
        }

    }
}
