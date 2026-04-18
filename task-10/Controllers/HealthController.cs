using Microsoft.AspNetCore.Mvc;

namespace BooksApi.Controllers;

/// <summary>
/// A simple health check endpoint — standard practice in microservices.
/// Load balancers and monitoring tools call this to verify the service is alive.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetHealth()
    {
        return Ok(new
        {
            Status = "Healthy",
            Service = "BooksApi",
            Timestamp = DateTime.UtcNow,
            Version = "1.0.0"
        });
    }
}