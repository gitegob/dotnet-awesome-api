using System.ComponentModel.DataAnnotations;
using Dotnet_API.Enums;
using Dotnet_API.Models;

namespace Dotnet_API.Dto;

public record CreateProductDto(
    [Required] string Name,
    [Required] string Slug,
    [Required] string Description,
    [EnumDataType(typeof(EProductType))]
    [Required]
    EProductType ProductType,
    [EnumDataType(typeof(EProductStatus)), Required]
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
    [EnumDataType(typeof(EProductStatus)),] EProductStatus? Status = null,
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

// public record ViewOnlyProductDto(
//     int Id,
//     string? Name,
//     string? Slug,
//     string? Description = null,
//     [EnumDataType(typeof(EProductType))] EProductType? ProductType = null,
//     [EnumDataType(typeof(EProductStatus)),] EProductStatus? Status = null,
//     double? Price = null,
//     bool? IsTaxable = null,
//     string? Image = null,
//     int? Quantity = null,
//     string? Unit = null,
//     bool? InStock = null
// );

public class ViewOnlyProductDtoClass
{
    public ViewOnlyProductDtoClass(Product product)
    {
        Id = product.Id;
        Name = product.Name;
        Slug = product.Slug;
        Description = product.Description;
        ProductType = product.ProductType;
        Status = product.Status;
        InStock = product.InStock;
        Image = product.Image;
        Price = product.Price;
        Quantity = product.Quantity;
    }
    public int? Id { get; set; }
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }

    [EnumDataType(typeof(EProductType))]
    public EProductType? ProductType { get; set; }

    [EnumDataType(typeof(EProductStatus))]
    public EProductStatus? Status { get; set; }
    public bool? InStock { get; set; }
    public string? Image { get; set; }
    public double? Price { get; set; }
    public int? Quantity { get; set; }
}