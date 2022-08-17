using Dotnet_API.Exceptions;
using Dotnet_API.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Dotnet_API.Controllers;

[ApiController]
[Route("/")]
public class MainController : ControllerBase
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly AppSettings _appSettings;

    private readonly ILogger<MainController> _logger;

    public MainController(ILogger<MainController> logger, IOptions<AppSettings> appSettings)
    {
        _logger = logger;
        _appSettings = appSettings.Value;
    }

    [HttpGet(Name = "hello")]
    public ActionResult<string> Get()
    {
        throw new NotFoundException();
        return "Hello";
    }
}