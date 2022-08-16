using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_API.Models;

[Table("categories")]
public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Details { get; set; }
    public string? Image { get; set; }
    public string? Icon { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}