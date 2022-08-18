using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_API.Entities;

[Table("shops")]
public class Shop : BaseEntity
{
    public string? Name { get; set; }
    public string? Slug { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; } = true;
    public string? CoverImage { get; set; }
    public string? Logo { get; set; }
    public string? StreetAddress { get; set; }
    public string? City { get; set; }
    public string? Contact { get; set; }
    public string? Website { get; set; }

    public int? OwnerId { get; set; }
    public virtual User? Owner { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}