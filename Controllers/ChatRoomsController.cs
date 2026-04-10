using Microsoft.AspNetCore.Mvc;
using online_chat.Contracts.Chat;
using online_chat.DTOs;
using online_chat.Interfaces.IChat;

namespace online_chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatRoomsController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatRoomsController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost]
        public async Task<ActionResult<ChatRoomDto>> CreateChatRoom([FromBody] CreateChatRequest request)
        {
            try
            {
                var createdChatRoom = await _chatService.CreateChatRoomAsync(request);
                return StatusCode(StatusCodes.Status201Created, createdChatRoom);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ChatRoomDto>>> GetAllChatRooms()
        {
            var chatRooms = await _chatService.GetAllChatRoomsAsync();
            return Ok(chatRooms);
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<IReadOnlyList<ChatRoomDto>>> GetChatRoomByUser(Guid userId)
        {
            try
            {
                var chatRooms = await _chatService.GetChatsByUserAsync(userId);
                return Ok(chatRooms);
            }
            catch (KeyNotFoundException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet("by-name/{chatName}")]
        public async Task<ActionResult<ChatRoomDto>> GetByName(string chatName)
        {
            var chat = await _chatService.GetChatByNameAsync(chatName);

            if (chat is null)
                return NotFound();

            return Ok(chat);
        }
    }
}