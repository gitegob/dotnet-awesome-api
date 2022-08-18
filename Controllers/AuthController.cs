using Dotnet_API.Dto;
using Dotnet_API.Entities;
using Dotnet_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API.Controllers;

[ApiController]
[Route("/api/v1/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService) => _authService = authService;

    [HttpPost("signup")]
    [ProducesResponseType(201)]
    public async Task<ActionResult<ApiResponse<User>>> Register(UserSignupDto userSignupDto)
    {
        var result = await _authService.Signup(userSignupDto);
        return Created(nameof(Register), new ApiResponse("Signup successful", result));
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<string>>> Login(UserLoginDto userLoginDto)
    {
        var result = await _authService.Login(userLoginDto);
        return Ok(new ApiResponse("Login successful", result));
    }
}