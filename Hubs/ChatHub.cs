using System.Text.Json;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using online_chat.Contracts.Chat;
using online_chat.Contracts.Message;
using online_chat.Interfaces.IChat;
using online_chat.Interfaces.IMessage;

namespace online_chat.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IDistributedCache _cache;
        private readonly IMessageService _messageService;
        private readonly IChatService _chatService;

        public ChatHub(
            IDistributedCache cache,
            IMessageService messageService,
            IChatService chatService)
        {
            _cache = cache;
            _messageService = messageService;
            _chatService = chatService;
        }

        public async Task JoinChat(JoinChatRequest request)
        {
            await _chatService.AddUserToChatAsync(request.UserId, request.ChatRoomId);

            var groupName = request.ChatRoomId.ToString();

            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

            var serialized = JsonSerializer.Serialize(request);
            await _cache.SetStringAsync(Context.ConnectionId, serialized);

            await Clients
                .Group(groupName)
                .ReceiveSystemMessage("Admin", $"{request.UserName} has joined the chat");


        }

        public async Task SendMessage(string messageText)
        {
            var connection = await GetConnectionAsync();

            if (connection is null)
            {
                return;
            }

            var request = new SaveMessageRequest
            {
                ChatRoomId = connection.ChatRoomId,
                UserId = connection.UserId,
                Text = messageText
            };

            var savedMessage = await _messageService.AddMessageAsync(request);

            await Clients
                .Group(request.ChatRoomId.ToString())
                .ReceiveMessage(savedMessage);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var connection = await GetConnectionAsync();

            if (connection is not null)
            {
                await Clients
                    .Group(connection.ChatRoomId.ToString())
                    .ReceiveSystemMessage("Admin", $"{connection.UserName} has left the chat");

                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoomId.ToString());


            }

            await base.OnDisconnectedAsync(exception);
        }

        private async Task<JoinChatRequest?> GetConnectionAsync()
        {
            var json = await _cache.GetStringAsync(Context.ConnectionId);

            if (string.IsNullOrWhiteSpace(json))
            {
                return null;
            }

            return JsonSerializer.Deserialize<JoinChatRequest>(json);
        }
    }
}