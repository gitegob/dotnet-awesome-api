using Dotnet_API.Authorization;
using Dotnet_API.Dto;
using Dotnet_API.Exceptions;
using Dotnet_API.Models;
using Dotnet_API.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Dotnet_API.Services;

public class ProductService
{
    private readonly DatabaseContext _db;
    public ProductService(DatabaseContext dbContext) => (_db) = (dbContext);

    public async Task<Product> CreateProduct(CreateProductDto dto)
    {
        var userId = (int)CurrentUser.Get("id");
        var newProduct = new Product
        {
            Name = dto.Name,
            Slug = dto.Slug,
            Description = dto.Description,
            ProductType = dto.ProductType,
            Status = dto.Status,
            Price = dto.Price,
            IsTaxable = dto.IsTaxable,
            SalePrice = dto.SalePrice,
            MinPrice = dto.MinPrice,
            MaxPrice = dto.MaxPrice,
            Gallery = dto.Gallery ?? Array.Empty<string>(),
            Image = dto.Image,
            Height = dto.Height,
            Length = dto.Length,
            Width = dto.Width,
            Quantity = dto.Quantity,
            Unit = dto.Unit,
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
        var query = _db.Products.Where(s => !s.IsDeleted).OrderByDescending(s => s.CreatedAt)
            .Select(p => new ViewOnlyProductDto(p.Id,p.Name,p.Slug,p.Description,p.ProductType,p.Status,p.Price,p.Image,p.Quantity,p.InStock));
        var result = await PaginationUtil.Paginate(query, paginationParams.Page, paginationParams.Size);
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
        return new ViewOnlyProductDto(product.Id,product.Name,product.Slug,product.Description,product.ProductType,product.Status,product.Price,product.Image,product.Quantity,product.InStock);
    }

    // public async Task<Product?> UpdateProduct(int id, UpdateProductDto productDto)
    // {
    //     var product = await GetProduct(id);
    //     if (product == null) return null;
    //     if (productDto.Model != null) product.Model = productDto.Model;
    //     if (productDto.Price != null) product.Price = productDto.Price;
    //     if (productDto.Colors != null) product.Colors = productDto.Colors;
    //     if (productDto.InStock != null) product.InStock = productDto.InStock;
    //     await _db.SaveChangesAsync();
    //     return product;
    // }

    public async Task RemoveProduct(int id)
    {
        var product = await GetOne(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}