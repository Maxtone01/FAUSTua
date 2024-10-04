using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers;

public class LibraryController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}