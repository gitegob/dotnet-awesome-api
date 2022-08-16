namespace Dotnet_API.Authorization;

/**
 * Jwt payload
 */
public class JwtPayload
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}