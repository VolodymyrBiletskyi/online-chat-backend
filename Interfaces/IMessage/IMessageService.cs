using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.Message;
using online_chat.DTOs;

namespace online_chat.Interfaces.IMessage
{
    public interface IMessageService
    {
        Task<IReadOnlyList<MessageDto>> GetMessagesByChatRoomIdAsync(Guid chatRoomId);
        Task<MessageDto> AddMessageAsync(SaveMessageRequest request);
    }
}