using FaustWeb.Application.Services.AuthService;
using FaustWeb.Application.Services.EmailService;
using FaustWeb.Domain.DTO.Auth;
using FaustWeb.Domain.DTO.Email;
using FaustWeb.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [TypeFilter(typeof(ApiControllerExceptionFilter))]
    public class ApiController(IAuthService authService, IEmailService emailService) : ControllerBase
    {
        [HttpGet("test")]
        [ApiExplorerSettings(GroupName = "Test")]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpGet("test-filter")]
        [ApiExplorerSettings(GroupName = "Test")]
        public IActionResult TestFilter()
        {
            throw new NotImplementedException();
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

        [HttpPost("send-email")]
        [ApiExplorerSettings(GroupName = "Email")]
        public async Task<IActionResult> SendEmail()
        {
            var recipients = new List<string> { "faustua2024@gmail.com" };
            var message = new EmailMessage(recipients, "Test email", "Test email content");

            await emailService.SendTextEmailAsync(message);
            return Ok("Email Sent");
        }

        [HttpPost("forgot-password")]
        [ApiExplorerSettings(GroupName = "Email")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var response = await authService.ForgotPassword(forgotPasswordDto);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        [ApiExplorerSettings(GroupName = "Email")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            await authService.ResetPassword(resetPasswordDto);
            return Ok("Password is reset");
        }
    }
}
