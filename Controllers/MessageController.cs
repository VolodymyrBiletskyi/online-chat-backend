using Microsoft.AspNetCore.Mvc;
using online_chat.DTOs;
using online_chat.Interfaces;
using online_chat.Interfaces.IMessage;

namespace online_chat.Controllers
{
    [Route("api/chats/{chatRoomId:guid}/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MessageDto>>> GetMessagesByChatRoomId(Guid chatRoomId)
        {
            var messages = await _messageService.GetMessagesByChatRoomIdAsync(chatRoomId);
            return Ok(messages);
        }
    }
}