namespace Dotnet_API.Utils;

public static class CurrentUser
{
    public static dynamic Get(string propertyName)
    {
        return new HttpContextAccessor().HttpContext.User.Claims.FirstOrDefault(x => x.Type == propertyName).Value;
    }
}