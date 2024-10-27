using FaustWeb.Domain.DTO.Auth;
using System.Security.Claims;

namespace FaustWeb.Application.Services.AuthService;

public interface IAuthService
{
    Task<ClaimsIdentity> Login(LoginDto loginDto);
    Task<ClaimsIdentity> Signup(RegisterDto registerDto);
    Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto);
    Task<IEnumerable<string>> ResetPassword(ResetPasswordDto resetPasswordDto);
}
