using Dotnet_API.Dto;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_API.Utils;

public class PaginationUtil
{
    public static async Task<Page<T>> Paginate<T>(IQueryable<T> queryable, int page, int size)
    {
        var result = await queryable
            .Skip(size * page)
            .Take(size)
            .ToListAsync();
        return new Page<T>(result, page, size, queryable.Count());
    }

}