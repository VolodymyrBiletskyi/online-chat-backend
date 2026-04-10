using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Contracts.User;
using online_chat.DTOs;
using online_chat.Interfaces.IUser;
using online_chat.Mappers;


namespace online_chat.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<UserDto> AddUserAsync(CreateUser createUser)
        {
            if (string.IsNullOrWhiteSpace(createUser.Username))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            var userName = createUser.Username.Trim().ToLowerInvariant();

            if (await _userRepo.GetByUsernameAsync(userName) != null)
            {
                throw new InvalidOperationException("Username already exists.");
            }

            var user = UserMapper.ToEntity(createUser);

            await _userRepo.AddUser(user);
            await _userRepo.SaveChangesAsync();

            return UserMapper.ToDto(user);
        }

        public async Task<UserDto?> GetByIdAsync(Guid userId)
        {
            var user = await _userRepo.GetByIdAsync(userId);

            return user is null ? null : UserMapper.ToDto(user);
        }

        public async Task<UserDto?> GetByUsernameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            var normalizedName = userName.Trim().ToLowerInvariant();

            var user = await _userRepo.GetByUsernameAsync(normalizedName);

            return user is null ? null : UserMapper.ToDto(user);

        }
        public async Task<UserDto> LoginAsync(LoginUser login)
        {
            if (string.IsNullOrWhiteSpace(login.Username))
            {
                throw new ArgumentException("Username cannot be empty.");
            }

            var normalizedUsername = login.Username.Trim().ToLowerInvariant();

            var existingUser = await _userRepo.GetByUsernameAsync(normalizedUsername);

            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            return UserMapper.ToDto(existingUser);
        }
    }
}