using AutoMapper.QueryableExtensions;
using Dotnet_API.Attributes;
using Dotnet_API.Dto;
using Dotnet_API.Entities;

namespace Dotnet_API.Services;
[ScopedService]
public class CategoryService
{
    private readonly DatabaseContext _db;

    public CategoryService(DatabaseContext db) => _db = db;

    public async Task<Page<ViewCategoryDto>> GetCategories(PaginationParams paginationParams)
    {
        var query = _db.Categories
            .Where(s => !s.IsDeleted)
            .OrderByDescending(s => s.CreatedAt)
            .ProjectTo<ViewCategoryDto>(MappingUtil.Map<Category, ViewCategoryDto>());
        return await PaginationUtil.Paginate(query, paginationParams.page, paginationParams.size);
    }
}