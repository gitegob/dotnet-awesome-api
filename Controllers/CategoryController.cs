using Dotnet_API.Dto;
using Dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API.Controllers;

[ApiController,Route("/api/v1/categories")]
[Authorize]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _shopService;

    public CategoryController(CategoryService shopService) => (_shopService) = (shopService);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<Page<ViewCategoryDto>>>> GetCategories([FromQuery] PaginationParams paginationParams)
    {
        var result = await _shopService.GetCategories(paginationParams);
        return Ok(new ApiResponse("Categories retrieved successfully", result));
    }
}