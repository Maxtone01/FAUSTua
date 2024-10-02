using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers;

public class AuthenticationController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}