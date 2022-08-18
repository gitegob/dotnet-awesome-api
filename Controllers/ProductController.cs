using Dotnet_API.Dto;
using Dotnet_API.Entities;
using Dotnet_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_API.Controllers;

[ApiController]
[Route("/api/v1/products")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<ActionResult<Product>> CreateProduct(CreateProductDto dto)
    {
        var result = await _productService.CreateProduct(dto);
        return Created(nameof(CreateProduct), new ApiResponse("Product created", result));
    }

    [HttpGet]
    public async Task<ActionResult<Page<ViewOnlyProductDto>>> GetProducts(
        [FromQuery] PaginationParams paginationParams)
    {
        var result = await _productService.GetProducts(paginationParams);
        return Ok(new ApiResponse("Products retrieved", result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ViewOnlyProductDto>> GetProduct(int id)
    {
        var result = await _productService.GetProduct(id);
        return Ok(new ApiResponse("Product retrieved", result));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduct(int id, UpdateProductDto dto)
    {
        var result = await _productService.UpdateProduct(id,dto);
        return Ok(new ApiResponse("Product updated", result));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> RemoveProduct(int id)
    {
        await _productService.RemoveProduct(id);
        return Ok(new ApiResponse("Product retrieved"));
    }
}