using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class HealthController : Controller
    {
        // GET api/health
        [HttpGet]
        public IActionResult Get()
            => Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}
