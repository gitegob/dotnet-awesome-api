using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Dotnet_API.Attributes;
using Dotnet_API.Entities;
using Dotnet_API.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Extensions;

namespace Dotnet_API.Authorization;

[ScopedService]
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
            new("id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.GetDisplayName())
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddDays(1), signingCredentials: creds);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
        return jwtToken;
    }

    public JwtPayload? ValidateToken(string token)
    {
        if (token == null)
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.Key);
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            }, out var validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            // return user id from JWT token if validation successful
            return new JwtPayload
            {
                Id = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value),
                Email = jwtToken.Claims.First(x => x.Type == "email").Value,
                Role = jwtToken.Claims.First(x => x.Type == "role").Value
            };
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}