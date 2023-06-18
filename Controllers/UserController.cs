using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostOpinion.Domain.Entities;
using PostOpinion.DTO;
using PostOpinion.Interfaces;
using PostOpinion.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;

namespace PostOpinion.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger; 
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;            
            _logger = logger;
        }

        [HttpGet]
        [Route("Users")]
        public async Task<List<User>> GetAllPosts() =>
    await _userService.GetUsersAsync();

        [HttpGet("{ID}/User")]
        public async Task<User> GetUserByIDAsync([FromRoute] int ID) =>
            await _userService.GetUserByIDAsync(ID);

        [HttpPost("Create User")]
        public async Task<User> CreateUserAsync([FromQuery] UserDTO userDTO) {
            User user = await _userService.CreateUserAsync(userDTO);
            //_logger.LogInformation(user.Email);
            //_logger.File
            _logger.LogInformation($"{user.Email}");
            return user;
        }
        

        [HttpPut("{ID}/Update User")]
        public async Task<IActionResult> UpdateUserAsync([FromRoute] int ID, [FromBody] UserDTO userDTO)
        {
            var value = await _userService.UpdateUserAsync(ID, userDTO);
            if (!value) return BadRequest();
            return Ok();
        }

        //logically renewed
        [HttpDelete("{ID}/Delete User")]
        public async Task<IActionResult> DeleteUserAsync([FromRoute] int ID)
        {
            var value = await _userService.DeleteUserAsync(ID);
            if (!value) return BadRequest();
            return Ok();
        }
    }
}
