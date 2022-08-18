using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet_API.Enums;

namespace Dotnet_API.Entities;

[Table("products")]
public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }

    [EnumDataType(typeof(EProductType))]
    [Column(TypeName = "varchar(255)")]
    public EProductType? Type { get; set; }

    public bool? InStock { get; set; }
    public string[] Images { get; set; } = Array.Empty<string>();

    [EnumDataType(typeof(EProductStatus))]
    [Column(TypeName = "varchar(255)")]
    public EProductStatus? Status { get; set; }

    public double? Price { get; set; }
    public int? Quantity { get; set; }
    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<ProductVariation> Variations { get; set; } = new List<ProductVariation>();
    public int? ShopId { get; set; }
    [JsonIgnore] public virtual Shop? Shop { get; set; }
}