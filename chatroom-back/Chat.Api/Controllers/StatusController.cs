using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

/// <summary>
/// Provides endpoints for controlling the status and health of the platform.
/// </summary>
[ApiController]
public sealed class StatusController : ControllerBase
{
    /// <summary>
    /// Gets the status of the platform.
    /// </summary>
    /// <returns>The status of the platform.</returns>
    /// <response code="200">Platform is up.</response>
    [HttpGet("/api/status")]
    public ActionResult GetStatus()
    {
        return Ok("Healthy");
    }
}