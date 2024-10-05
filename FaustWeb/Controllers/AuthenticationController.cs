using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers;

[Route("authentication")]
public class AuthenticationController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Auth()
    {
        return View();
    }
}