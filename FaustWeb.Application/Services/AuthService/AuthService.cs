using FaustWeb.Application.Services.EmailService;
using FaustWeb.Domain.DTO.Auth;
using FaustWeb.Domain.DTO.Email;
using FaustWeb.Domain.Helpers;
using FaustWeb.SeedData.DefaultIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FaustWeb.Application.Services.AuthService;

public class AuthService(UserManager<IdentityUser> userManager, IEmailService emailService,
    IHttpContextAccessor httpContextAccessor) : IAuthService
{
    public async Task<ClaimsIdentity> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            throw new NullReferenceException("User does not exist");

        if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
            throw new Exception("Invalid password");

        return Authenticate(user);
    }

    public async Task<ClaimsIdentity> Signup(RegisterDto registerDto)
    {
        var user = await userManager.FindByEmailAsync(registerDto.Email);
        if (user != null)
            throw new Exception("User is already exists");

        if (registerDto.Password != registerDto.RepeatPassword)
            throw new Exception("Password and confirmation password do not match");

        var newUser = new IdentityUser
        {
            UserName = registerDto.Nickname,
            Email = registerDto.Email,
        };

        var response = await userManager.CreateAsync(newUser, registerDto.Password);

        if (!response.Succeeded)
        {
            var errorsMessages =
                response.Errors.Aggregate("", (current, error) => current + " " + error.Description);

            throw new Exception(errorsMessages);
        }

        await userManager.AddToRoleAsync(newUser, DefaultRoles.User);

        var appUser = new LoginDto
        {
            Email = registerDto.Email,
            Password = registerDto.Password,
        };

        return await Login(appUser);
    }

    public async Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null)
            throw new NullReferenceException("User does not exist");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var parameters = new Dictionary<string, string>
        {
            { "token", token },
            { "email", forgotPasswordDto.Email }
        };

        var clientUri = HttpContextHelper.GetClientUri(httpContextAccessor.HttpContext!);
        var request = $"{clientUri}?token={parameters["token"]}&email={parameters["email"]}";
        var message = new EmailMessage([user.Email!], "Reset password", request);

        await emailService.SendEmailAsync(message);
        return request;
    }

    public async Task ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
        if (user == null)
            throw new NullReferenceException("User does not exist");

        var response = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password);
        if (!response.Succeeded)
        {
            var errors = response.Errors.Select(x => x.Description);
            throw new InvalidOperationException(errors.FirstOrDefault());
        }
    }

    private ClaimsIdentity Authenticate(IdentityUser user)
    {
        var role = userManager.GetRolesAsync(user).Result.FirstOrDefault();
        var claims = new List<Claim>
        {
            new(ClaimsIdentity.DefaultNameClaimType, user.Email!),
            new(ClaimsIdentity.DefaultRoleClaimType, role!),
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
