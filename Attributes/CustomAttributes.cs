namespace Dotnet_API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class ScopedServiceAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class SingletonServiceAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class TransientServiceAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class MiddlewareAttribute : Attribute
{
}