namespace Dotnet_API.Dto;

public record ViewOnlyShopDto(
    int Id,
    string? Name,
    string? Slug,
    string? CoverImage,
    string? Logo
);