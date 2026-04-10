using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using online_chat.Models;

namespace online_chat.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByUsernameAsync(string username);
        Task<int> SaveChangesAsync();
    }
}