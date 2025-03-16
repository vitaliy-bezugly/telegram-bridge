using Microsoft.AspNetCore.Mvc;
using TelegramBridge.Api.Constants;

namespace TelegramBridge.Api.Controllers;

[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet, Route(RouteConstants.Ping.Get)]
    public IActionResult Ping()
    {
        return Ok(new { status = "pong" });
    }
}
