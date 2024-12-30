using FaustWeb.Application.Services.AuthService;
using FaustWeb.Domain.DTO.Auth;
using FaustWeb.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FaustWeb.Controllers;

[Route("authentication")]
[TypeFilter(typeof(MvcExceptionFilter))]
public class AuthenticationController(IAuthService authService) : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return View(loginDto);
        }

        var response = await authService.Login(loginDto);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(response));

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("signup")]
    public IActionResult Signup()
    {
        return View();
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return View(registerDto);
        }

        var response = await authService.Signup(registerDto);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(response));

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("reset-password")]
    public IActionResult ResetPassword([FromQuery] string token, [FromQuery] string email)
    {
        var model = new ResetPasswordDto
        {
            Token = token,
            Email = email
        };

        return View(model);
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        if (!ModelState.IsValid)
        {
            return View(resetPasswordDto);
        }

        var result = await authService.ResetPassword(resetPasswordDto);

        if (result.Any())
        {
            foreach (var error in result)
            {
                ModelState.AddModelError(error, error);
            }

            return View(resetPasswordDto);
        }

        return RedirectToAction(nameof(Login));
    }

    [HttpGet("logout")]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}