using AutoMapper;
using AutoMapper.Internal;
using AutoMapper.QueryableExtensions;
using Dotnet_API.Attributes;
using Dotnet_API.Authorization;
using Dotnet_API.Dto;
using Dotnet_API.Entities;
using Dotnet_API.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet_API.Services;

[ScopedService]
public class ProductService
{
    private readonly DatabaseContext _db;

    public ProductService(DatabaseContext dbContext)
    {
        _db = dbContext;
    }

    public async Task<Product> CreateProduct(CreateProductDto dto)
    {
        var userId = (int)int.Parse(CurrentUser.Get("id"));
        var newProduct = new Product
        {
            Name = dto.Name,
            Slug = dto.Slug,
            Description = dto.Description,
            Type = dto.Type,
            Status = dto.Status,
            Price = dto.Price,
            Images = dto.Images ?? Array.Empty<string>(),
            Quantity = dto.Quantity,
            InStock = dto.InStock
        };
        if (dto.ShopId != null)
        {
            var shop = await _db.Shops
                .Where(s => !s.IsDeleted)
                .Where(s => s.Id.Equals(dto.ShopId) && s.OwnerId.Equals(userId))
                .FirstOrDefaultAsync();
            if (shop != null)
            {
                newProduct.ShopId = shop.Id;
                newProduct.Shop = shop;
            }
        }

        if (!dto.Categories.IsNullOrEmpty())
        {
            var categories = await _db.Categories.Where(c => !c.IsDeleted && dto.Categories.Contains(c.Id))
                .ToListAsync();
            if (!categories.IsNullOrEmpty()) newProduct.Categories = categories;
        }

        _db.Products.Add(newProduct);
        await _db.SaveChangesAsync();
        return newProduct;
    }

    public async Task<Page<ViewOnlyProductDto>> GetProducts(PaginationParams paginationParams)
    {
        var mapping = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Product, ViewOnlyProductDto>();
            cfg.CreateMap<Shop, ViewOnlyShopDto>();
            cfg.CreateMap<Category, ViewCategoryDto>();
        });
        var query = _db.Products
            .Include(p => p.Shop)
            .Include(p => p.Categories)
            .Where(s => !s.IsDeleted).OrderByDescending(s => s.CreatedAt)
            .ProjectTo<ViewOnlyProductDto>(mapping);
        var result = await PaginationUtil.Paginate(query, paginationParams.page, paginationParams.size);
        return result;
    }

    public async Task<Product?> GetOne(int id)
    {
        var product = await _db.Products.FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
        if (product == null) throw new NotFoundException("Product not found");
        return product;
    }

    public async Task<ViewOnlyProductDto?> GetProduct(int id)
    {
        var product = await GetOne(id);
        if (product == null) throw new NotFoundException("Product not found");
        return new ViewOnlyProductDto(product.Id, product.Name, product.Slug, product.Description, product.Type,
            product.Status, product.Price, product.Images, product.InStock);
    }

    public async Task<Product?> UpdateProduct(int id, UpdateProductDto productDto)
    {
        var product = await GetOne(id);
        if (productDto.Name != null) product.Name = productDto.Name;
        if (productDto.Slug != null) product.Slug = productDto.Slug;
        if (productDto.Description != null) product.Description = productDto.Description;
        if (productDto.Type != null) product.Type = productDto.Type;
        if (productDto.Status != null) product.Status = productDto.Status;
        if (productDto.Price != null) product.Price = productDto.Price;
        if (productDto.Images != null) product.Images = productDto.Images;
        if (productDto.Quantity != null) product.Quantity = productDto.Quantity;
        if (productDto.InStock != null) product.InStock = productDto.InStock;

        if (productDto.Categories != null)
        {
            var categories = await _db.Categories.Where(c => !c.IsDeleted)
                .Where(c => productDto.Categories.Contains(c.Id))
                .ToListAsync();
            if (!categories.IsNullOrEmpty()) product.Categories = categories;
        }

        if (productDto.ShopId != null)
        {
            var userId = (int)CurrentUser.Get("id");
            var shop = await _db.Shops
                .Where(s => !s.IsDeleted)
                .Where(s => s.Id.Equals(productDto.ShopId) && s.OwnerId.Equals(userId))
                .FirstOrDefaultAsync();
            if (shop == null) throw new NotFoundException("Shop not found");
            product.ShopId = shop.Id;
            product.Shop = shop;
        }

        await _db.SaveChangesAsync();
        return product;
    }

    public async Task RemoveProduct(int id)
    {
        var product = await GetOne(id);
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
    }
}