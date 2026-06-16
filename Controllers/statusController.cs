using Microsoft.AspNetCore.Mvc;

namespace admission_validation.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { status = "OK" });
        }
    }
}
