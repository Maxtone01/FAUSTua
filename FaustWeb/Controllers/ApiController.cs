using FaustWeb.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FaustWeb.Controllers
{
    [Route("api")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [TypeFilter(typeof(ApiControllerExceptionFilter))]
    public class ApiController : ControllerBase
    {
        [HttpGet("test")]
        [ApiExplorerSettings(GroupName = "Test")]
        public IActionResult Test()
        {
            return Ok();
        }

        [HttpGet("test-filter")]
        [ApiExplorerSettings(GroupName = "Test")]
        public IActionResult TestFilter()
        {
            throw new NotImplementedException();
        }
    }
}
