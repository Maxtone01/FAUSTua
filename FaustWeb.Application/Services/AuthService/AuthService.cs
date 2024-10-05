using AutoMapper;
using FaustWeb.Domain.DTO.Auth;
using FaustWeb.SeedData.DefaultIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FaustWeb.Application.Services.AuthService;

public class AuthService(UserManager<IdentityUser> userManager) : IAuthService
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

    private ClaimsIdentity Authenticate(IdentityUser user)
    {
        var role = userManager.GetRolesAsync(user).Result.FirstOrDefault();
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
        };
        return new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
    }
}
