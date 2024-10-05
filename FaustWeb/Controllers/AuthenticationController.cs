using FaustWeb.Application.Services.AuthService;
using FaustWeb.Domain.DTO.Auth;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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

    [HttpPost]
    public async Task<IActionResult> Auth(AuthDto authDto)
    {
        if (!authDto.IsRegistration)
        {
            var response = await authService.Login(authDto.Login);
            if (response.IsAuthenticated)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response));
                return RedirectToAction("Index", "Home");
            }
            else
                return View(authDto);
        }
        else
        {
            var response = await authService.Signup(authDto.Register);
            if (response.IsAuthenticated)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(response));
                return RedirectToAction("Index", "Home");
            }
            else
                return View(authDto);
        }
    }

    [HttpGet("logout")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}