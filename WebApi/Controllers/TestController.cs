using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAuthController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet("public")]
        public IActionResult Public()
        {
            return this.Ok("Public endpoint:");
        }

        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return this.Ok("Protected endpoint:");
        }

        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return this.Ok("Admin endpoint:");
        }
    }
}
