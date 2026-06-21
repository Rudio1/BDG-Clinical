using Microsoft.AspNetCore.Mvc;

namespace BGD.CLINICAL.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            service = "BGD.CLINICAL.WebApi"
        });
    }
}
