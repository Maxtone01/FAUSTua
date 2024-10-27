using FaustWeb.Application.Services.AuthService;
using FaustWeb.Domain.DTO.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FaustWeb.Controllers;

[Route("authentication")]
public class AuthenticationController(IAuthService authService) : Controller
{
    [HttpGet]
    public IActionResult Auth()
    {
        return View();
    }

    [HttpGet("resetPassword")]
    public IActionResult ResetPassword([FromQuery] string token, [FromQuery] string email)
    {
        ResetPasswordDto model = new() { Token = token, Email = email };

        return View(model);
    }

    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return View(resetPasswordDto);
        }
        
        var result = await authService.ResetPassword(resetPasswordDto);

        if (result.Any())
        {
            foreach(var error in result)
            {
                ModelState.AddModelError(error, error);
            }

            return View(resetPasswordDto);
        }

        return RedirectToAction(nameof(Auth));
    }

    [HttpPost]
    public async Task<IActionResult> Auth(AuthDto authDto)
    {
        ClaimsIdentity response = authDto.IsRegistration
            ? await authService.Signup(authDto.Register)
            : await authService.Login(authDto.Login);

        if (response.IsAuthenticated)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(response));
            return RedirectToAction("Index", "Home");
        }

        return View(authDto);
    }

    [HttpGet("logout")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}