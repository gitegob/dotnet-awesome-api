using System.ComponentModel.DataAnnotations;
using Dotnet_API.Enums;

namespace Dotnet_API.Dto;

public record CreateProductDto(
    [Required] string Name,
    [Required] string Slug,
    [Required] string Description,
    [EnumDataType(typeof(EProductType))]
    [Required]
    EProductType ProductType,
    [EnumDataType(typeof(EProductStatus))]
    [Required]
    EProductStatus Status,
    [Required] double Price,
    [Required] ICollection<int> Categories,
    int? ShopId = null,
    bool? IsTaxable = null,
    double? SalePrice = null,
    double? MaxPrice = null,
    double? MinPrice = null,
    string[]? Gallery = null,
    string? Image = null,
    int? Height = null,
    int? Length = null,
    int? Width = null,
    int? Quantity = null,
    string? Unit = null,
    bool? InStock = null
);

public record UpdateProductDto(
    [Required] string Name,
    string? Slug = null,
    string? Description = null,
    [EnumDataType(typeof(EProductType))] EProductType? ProductType = null,
    [EnumDataType(typeof(EProductStatus))] EProductStatus? Status = null,
    double? Price = null,
    int? ShopId = null,
    ICollection<int>? Categories = null,
    bool? IsTaxable = null,
    double? SalePrice = null,
    double? MaxPrice = null,
    double? MinPrice = null,
    string[]? Gallery = null,
    string? Image = null,
    int? Height = null,
    int? Length = null,
    int? Width = null,
    int? Quantity = null,
    string? Unit = null,
    bool? InStock = null
);

public record ViewOnlyProductDto(
    int Id,
    string? Name,
    string? Slug,
    string? Description = null,
    [EnumDataType(typeof(EProductType))] EProductType? ProductType = null,
    [EnumDataType(typeof(EProductStatus))] EProductStatus? Status = null,
    double? Price = null,
    string? Image = null,
    int? Quantity = null,
    bool? InStock = null
);