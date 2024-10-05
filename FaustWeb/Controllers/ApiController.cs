using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ApiController : ControllerBase
    {
        [HttpGet("test")]
        [ApiExplorerSettings(GroupName = "Test")]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
