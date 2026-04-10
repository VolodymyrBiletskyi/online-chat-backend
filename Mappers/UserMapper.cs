using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.User;
using online_chat.DTOs;
using online_chat.Models;

namespace online_chat.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                Username = user.Username,
                CreatedAt = user.CreatedAt
            };
        }

        public static User ToEntity(CreateUser createUser)
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                Username = createUser.Username,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}