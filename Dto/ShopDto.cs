using Dotnet_API.Models;

namespace Dotnet_API.Dto;

public record ViewOnlyShopDto(
    int Id,
    string? Name,
    string? Slug,
    string? Description,
    bool? IsActive,
    string? CoverImage,
    string? Logo,
    string? StreetAddress,
    string? City,
    string? Contact,
    string? Website
);