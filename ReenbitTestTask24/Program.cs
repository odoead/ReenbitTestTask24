using Microsoft.EntityFrameworkCore;
using ReenbitTestTask24.DB;
using ReenbitTestTask24.Hub;
using ReenbitTestTask24.Interfaces;
using ReenbitTestTask24.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR().AddAzureSignalR(o=> o.ConnectionString= "Endpoint=https://reenbittesttask.service.signalr.net;AccessKey=8LEd0D4edv9r2DRoKzQe2qEaSqyWzW8BxcUuBjsgJbSaBfVsehsmJQQJ99ALACYeBjFXJ3w3AAAAASRSwSz6;Version=1.0;");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.WithOrigins("http://localhost:4200", "https://lively-stone-0a25e3c0f.4.azurestaticapps.net")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials());
});

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<ISentimentService>(provider =>
{
    var endpoint = builder.Configuration.GetSection("SentimentService")["Endpoint"];
    var key = builder.Configuration.GetSection("SentimentService")["Key"];
    return new SentimentService(endpoint, key);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapHub<MessageHub>("/chatHub");

app.MapControllers();

app.Run();
