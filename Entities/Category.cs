using System.ComponentModel.DataAnnotations.Schema;
using Dotnet_API.Entities;

namespace Dotnet_API.Entities;

[Table("categories")]
public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Details { get; set; }
    public string? Image { get; set; }
    public string? Icon { get; set; }
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}