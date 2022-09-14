using AutoMapper.QueryableExtensions;
using Dotnet_API.Attributes;
using Dotnet_API.Dto;
using Dotnet_API.Entities;

namespace Dotnet_API.Services;

[ScopedService]
public class ShopService
{
    private readonly DatabaseContext _db;

    public ShopService(DatabaseContext db) => _db = db;

    public async Task<Page<ViewOnlyShopDto>> GetShops(PaginationParams paginationParams)
    {
        var query = _db.Shops
            .Where(s => !s.IsDeleted)
            .OrderByDescending(s => s.CreatedAt)
            .ProjectTo<ViewOnlyShopDto>(MappingUtil.Map<Shop, ViewOnlyShopDto>());
        return await PaginationUtil.Paginate(query, paginationParams.page, paginationParams.size);
    }
}