using System.ComponentModel.DataAnnotations;
using Dotnet_API.Entities;
using Dotnet_API.Enums;

namespace Dotnet_API.Dto;

public record CreateProductDto(
    [Required] string Name,
    [Required] string Slug,
    [Required] string Description,
    [EnumDataType(typeof(EProductType))]
    [Required]
    EProductType Type,
    [EnumDataType(typeof(EProductStatus))]
    [Required]
    EProductStatus Status,
    [Required] double Price,
    [Required] ICollection<int> Categories,
    int? ShopId = null,
    string[]? Images = null,
    int? Quantity = null,
    bool? InStock = null
);

public record UpdateProductDto(
    string? Name,
    string? Slug = null,
    string? Description = null,
    [EnumDataType(typeof(EProductType))] EProductType? Type = null,
    [EnumDataType(typeof(EProductStatus))] EProductStatus? Status = null,
    double? Price = null,
    int? ShopId = null,
    ICollection<int>? Categories = null,
    string[]? Images = null,
    int? Quantity = null,
    bool? InStock = null
);

public record ViewOnlyProductDto(
    int Id,
    string? Name,
    string? Slug,
    string? Description = null,
    [EnumDataType(typeof(EProductType))] EProductType? Type = null,
    [EnumDataType(typeof(EProductStatus))] EProductStatus? Status = null,
    double? Price = null,
    string[]? Images = null,
    bool? InStock = null,
    ViewOnlyShopDto? Shop = null,
    List<ViewCategoryDto>? Categories = null

);