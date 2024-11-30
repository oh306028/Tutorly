using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tutorly.Application.Dtos.CreateDtos;
using Tutorly.Application.Dtos.DisplayDtos;
using Tutorly.WebAPI.Services;

namespace Tutorly.WebAPI.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService; 
        }


        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _userService.RegisterUserAsync(dto);

            return Created("/api/accounts", null);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginUserDto dto)
        {
            var result = await _userService.LoginUser(dto);

            return Ok(result);
        }

        [HttpDelete("userId")]
        public async Task<ActionResult> DeleteUser([FromRoute] int userId)
        {
            await _userService.DeleteUser(userId);

            return Ok();  
        
        }


        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDataDto>> GetUserData([FromRoute] int userId)
        {
            var result = await _userService.GetUserData(userId);

            return Ok(result);
        }


    }
}
