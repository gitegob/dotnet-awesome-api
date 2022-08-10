using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dotnet_API.Models;
using Dotnet_API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;

namespace Dotnet_API.Authorization;

public class JwtService
{
    private readonly AppSettings _appSettings;

    public JwtService(IOptions<AppSettings> appSettings)
    {
        _appSettings = appSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim("id", user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.GetDisplayName())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return jwtToken;
    }
}