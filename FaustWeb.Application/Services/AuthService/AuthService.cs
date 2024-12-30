using FaustWeb.Application.Services.EmailService;
using FaustWeb.Domain.DefaultIdentity;
using FaustWeb.Domain.DTO.Auth;
using FaustWeb.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Claims;

namespace FaustWeb.Application.Services.AuthService;

public class AuthService(UserManager<User> userManager, IEmailService emailService,
    IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator) : IAuthService
{
    public async Task<ClaimsIdentity> Login(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email)
            ?? throw new NullReferenceException("User does not exist");

        if (!await userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            throw new Exception("Invalid password");
        }

        return Authenticate(user);
    }

    public async Task<ClaimsIdentity> Signup(RegisterDto registerDto)
    {
        var user = await userManager.FindByEmailAsync(registerDto.Email);
        if (user != null)
        {
            throw new Exception("User is already exists");
        }

        if (registerDto.Password != registerDto.RepeatPassword)
        {
            throw new Exception("Password and confirmation password do not match");
        }

        var newUser = new User
        {
            UserName = registerDto.Nickname,
            Email = registerDto.Email,
            Tag = registerDto.Nickname
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
        var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email)
            ?? throw new NullReferenceException("User does not exist");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        Dictionary<string, string?> parameters = new()
        {
            { "token", token },
            { "email", forgotPasswordDto.Email }
        };

        string action = linkGenerator.GetUriByAction(httpContextAccessor.HttpContext!, "ResetPassword", "Authentication")!;

        string resetLink = QueryHelpers.AddQueryString(action, parameters);

        await emailService.SendPasswordResetEmailAsync(user.Email!, resetLink);
        return resetLink;
    }

    public async Task<IEnumerable<string>> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        List<string> errorsMessages = [];

        var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);

        if (user is null)
        {
            errorsMessages.Add("Помилка, спробуйте запросити процедуру скидання паролю ще раз.");
            return errorsMessages;
        }

        var response = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.Password!);

        if (!response.Succeeded)
        {
            errorsMessages = response.Errors.Select(x => x.Description).ToList();
        }

        return errorsMessages;
    }

    private ClaimsIdentity Authenticate(User user)
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
