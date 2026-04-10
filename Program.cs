using Microsoft.EntityFrameworkCore;
using online_chat.Data;
using online_chat.Hubs;
using online_chat.Interfaces.IChat;
using online_chat.Interfaces.IMessage;
using online_chat.Interfaces.ISentiment;
using online_chat.Interfaces.IUser;
using online_chat.Repositories;
using online_chat.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var cs = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(cs));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            "http://localhost:5173",
            "https://localhost:5173",
            "https://online-chat-frontend-dcgyhch0fngsf6bp.switzerlandnorth-01.azurewebsites.net"
            )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();

    });
});

builder.Services.AddSignalR()
    .AddAzureSignalR(builder.Configuration.GetConnectionString("AzureSignalR"));

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<ISentimentAnalysisService, SentimentService>();
builder.Services.AddScoped<IChatService, ChatRoomService>();
builder.Services.AddDistributedMemoryCache();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();


app.MapHub<ChatHub>("/chat");
app.MapControllers();

app.MapFallbackToFile("index.html");

app.Run();