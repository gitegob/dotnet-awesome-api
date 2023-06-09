using System.ComponentModel.DataAnnotations;
using Dotnet_API.Enums;

namespace Dotnet_API.Dto;

public record CreateUserDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required, EmailAddress] string Email,
    [Required] string Password,
    [Required] [Phone] string Phone,
    [Required] string Address,
    [Required]
    [EnumDataType(typeof(ERoles))]
    ERoles Role
);

public record UserSignupDto(
    [Required] string FirstName,
    [Required] string LastName,
    [Required, EmailAddress] string Email,
    [Required] string Password,
    [Required, Phone] string Phone,
    [Required] string Address
);

public record UserLoginDto(
    [Required] [EmailAddress] string Email,
    [Required] string Password
);

public record ViewUserDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    [EnumDataType(typeof(ERoles))] ERoles Role
)
{
    public string? FullName => $"{FirstName} {LastName}";
}