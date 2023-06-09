using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_API.Entities;

[Table("product_variations")]
public class ProductVariation : BaseEntity
{
    public string? Title { get; set; }
    public double? Price { get; set; }
    public bool? Disabled { get; set; }
    public int? Quantity { get; set; }

    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
}