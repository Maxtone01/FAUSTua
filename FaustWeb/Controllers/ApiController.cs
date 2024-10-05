using FaustWeb.Application.Services.AuthService;
using FaustWeb.Domain.DTO.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ApiController(IAuthService authService) : ControllerBase
    {
        [HttpGet("test")]
        [ApiExplorerSettings(GroupName = "Test")]
        public IActionResult Test()
        {
            return Ok();
        }


        [HttpGet("login")]
        [ApiExplorerSettings(GroupName = "Auth")]
        public async Task<IActionResult> Login([FromQuery] LoginDto loginDto)
        {
            var response = await authService.Login(loginDto);
            return Ok(response.IsAuthenticated);
        }

        [HttpGet("register")]
        [ApiExplorerSettings(GroupName = "Auth")]
        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
        {
            var response = await authService.Signup(registerDto);
            return Ok(response.IsAuthenticated);
        }
    }
}
