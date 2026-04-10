using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace online_chat.Contracts.User
{
    public class LoginUser
    {
        [Required]
        public string Username { get; set; } = null!;
    }
}