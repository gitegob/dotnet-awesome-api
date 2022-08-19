using AutoMapper;

namespace Dotnet_API.Utils;

public static class MappingUtil
{
    public static MapperConfiguration Map<TSource, TDestination>()
    {
        return new MapperConfiguration(cfg =>
            cfg.CreateMap<TSource, TDestination>());          
    }
}