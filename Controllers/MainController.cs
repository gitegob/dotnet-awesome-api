using Dotnet_API.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API.Controllers;

[ApiController]
[Route("/")]
public class MainController : ControllerBase
{

    [HttpGet(Name = "hello")]
    public ActionResult<string> Get() => "Hello";
}