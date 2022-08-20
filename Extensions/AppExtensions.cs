using Dotnet_API.Attributes;

namespace Dotnet_API.Extensions;

public static class AppExtensions
{
    public static void RegisterMiddleware(this WebApplication app)
    {
        var middlewareType = typeof(MiddlewareAttribute);
        var middlewareClasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(mc => mc.IsDefined(middlewareType, false) && !mc.IsInterface)
            .Select(mc => mc);
        
        foreach (var middlewareClass in middlewareClasses)
        {
            app.UseMiddleware(middlewareClass);
        }
    }
}