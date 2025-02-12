using Microsoft.AspNetCore.Mvc;

namespace Chatter.Api.Controllers;

[Route("health-check")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult HealthCheck()
    {
        return Ok();
    } 
}