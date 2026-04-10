using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.User;
using online_chat.DTOs;

namespace online_chat.Interfaces.IUser
{
    public interface IUserService
    {
        Task<UserDto> AddUserAsync(CreateUser createUser);
        Task<UserDto?> GetByIdAsync(Guid userId);
        Task<UserDto?> GetByUsernameAsync(string userName);
        Task<UserDto> LoginAsync(LoginUser login);

    }
}