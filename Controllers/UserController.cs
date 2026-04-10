using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using online_chat.Contracts.User;
using online_chat.DTOs;
using online_chat.Interfaces.IUser;

namespace online_chat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUser createUser)
        {
            try
            {
                var createdUser = await _userService.AddUserAsync(createUser);
                return CreatedAtAction(nameof(GetById), new { userId = createdUser.UserId }, createdUser);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginUser([FromBody] LoginUser login)
        {
            try
            {
                var createdUser = await _userService.LoginAsync(login);
                return Ok(createdUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid userId)
        {
            try
            {
                var user = await _userService.GetByIdAsync(userId);
                if (user is null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("by-username/{username}")]
        public async Task<ActionResult<UserDto>> GetByUsername(string username)
        {
            try
            {
                var user = await _userService.GetByUsernameAsync(username);
                if (user is null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}