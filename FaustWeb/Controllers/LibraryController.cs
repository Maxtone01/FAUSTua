using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers;

[Route("library")]
public class LibraryController : Controller
{
    [HttpGet]
    public async Task<IActionResult> Library()
    {
        return View();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> LibraryItem(int id)
    {
        return View();
    }
}