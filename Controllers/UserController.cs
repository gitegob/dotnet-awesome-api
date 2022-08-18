using Dotnet_API.Dto;
using Dotnet_API.Entities;
using Dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API.Controllers;

[ApiController]
[Route("/api/v1/users")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [ProducesResponseType(201)]
    public async Task<ActionResult<ApiResponse<User>>> CreateUser(CreateUserDto userRegisterDto)
    {
        var result = await _userService.RegisterUser(userRegisterDto);
        return CreatedAtAction(nameof(CreateUser), new ApiResponse("Registration successful", result));
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<Page<ViewUserDto>>>> GetUsers(
        [FromQuery] PaginationParams paginationParams)
    {
        var result = await _userService.GetUsers(paginationParams);
        return Ok(new ApiResponse("Users retrieved", result));
    }
}