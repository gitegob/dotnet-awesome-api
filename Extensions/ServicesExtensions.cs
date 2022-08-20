using Dotnet_API.Attributes;

namespace Dotnet_API.Extensions;

public static class ServicesExtensions
{
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        var scopedServiceType = typeof(ScopedServiceAttribute);
        var singletonServiceType = typeof(SingletonServiceAttribute);
        var transientServiceType = typeof(TransientServiceAttribute);
        var serviceClasses = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(sc => sc.IsDefined(scopedServiceType, false) || sc.IsDefined(transientServiceType, false) ||
                           sc.IsDefined(singletonServiceType, false) && !sc.IsInterface)
            .Select(sc => sc);
        
        foreach (var serviceClass in serviceClasses)
        {
            if (serviceClass.IsDefined(scopedServiceType, false))
            {
                services.AddScoped(serviceClass);
            }
        
            if (serviceClass.IsDefined(transientServiceType, false))
            {
                services.AddTransient(serviceClass);
            }
        
            if (serviceClass.IsDefined(singletonServiceType, false))
            {
                services.AddSingleton(serviceClass);
            }
        }
    }
}