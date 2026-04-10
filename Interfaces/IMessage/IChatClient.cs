using online_chat.DTOs;

public interface IChatClient
{
    public Task ReceiveSystemMessage(string userName, string message);
    public Task ReceiveMessage(MessageDto message);
}