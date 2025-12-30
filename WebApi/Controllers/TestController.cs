using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestAuthController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return this.Ok("Public endpoint:");
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return this.Ok("Protected endpoint:");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin")]
        public IActionResult Admin()
        {
            return this.Ok("Admin endpoint:");
        }
    }
}
