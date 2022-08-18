using Dotnet_API.Dto;
using Dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API.Controllers;

[ApiController, Route("/api/v1/shops")]
[Authorize]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;

    public ShopController(ShopService shopService) => (_shopService) = (shopService);

    [HttpGet]
    public async Task<ActionResult<ApiResponse<Page<ViewOnlyShopDto>>>> GetShops([FromQuery] PaginationParams paginationParams)
    {
        var result = await _shopService.GetShops(paginationParams);
        return Ok(new ApiResponse("Shops retrieved successfully", result));
    }
}