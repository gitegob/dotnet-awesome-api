using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Dotnet_API.Enums;

namespace Dotnet_API.Models;

[Table("users")]
public class User : BaseEntity
{
    public string? FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    [JsonIgnore] public string Password { get; set; } = string.Empty;

    public string? Phone { get; set; }
    public string? Address { get; set; } = string.Empty;

    [EnumDataType(typeof(ERoles))]
    [Column(TypeName = "varchar(255)")]
    public ERoles Role { get; set; } = ERoles.CLIENT;

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();
}