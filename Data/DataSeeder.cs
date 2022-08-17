using Bogus;
using Dotnet_API.Enums;
using Dotnet_API.Models;
using Dotnet_API.Settings;
using Microsoft.Extensions.Options;

namespace Dotnet_API.Data;

public class DataSeeder
{
    private readonly AppSettings _appSettings;
    private readonly DatabaseContext _dbContext;
    private readonly Faker _faker = new();

    public DataSeeder(DatabaseContext dbContext, IOptions<AppSettings> appSettings)
    {
        (_dbContext, _appSettings) = (dbContext, appSettings.Value);
    }

    public void Seed()
    {
        SeedUsers();
        SeedShops();
        SeedCategories();
        SeedProducts();
    }

    private void SeedUsers()
    {
        if (_dbContext.Users.Any()) return;
        var users = new List<User>
        {
            new()
            {
                FirstName = "Admin",
                LastName = "Dotnet",
                Email = "admin@dotnet.rw",
                Password = PasswordEncryption.HashPassword(_appSettings.DefaultPassword),
                Phone = "+250785721391",
                Address = "Kigali",
                Role = ERoles.ADMIN
            }
        };

        _dbContext.Users.AddRange(users);
        _dbContext.SaveChanges();
    }

    private void SeedShops()
    {
        var users = _dbContext.Users.ToList();
        if (_dbContext.Shops.Any()) return;
        var shops = Enumerable.Range(1, 10)
            .Select(i => new Shop
            {
                Name = _faker.Company.CompanyName(),
                Slug = _faker.Random.Word(),
                Description = _faker.Company.CatchPhrase(),
                CoverImage = _faker.Image.PicsumUrl(),
                Logo = _faker.Image.PicsumUrl(),
                StreetAddress = _faker.Address.StreetAddress(),
                City = _faker.Address.City(),
                Contact = _faker.Phone.PhoneNumber("+250 78# ### ###"),
                Website = _faker.Internet.Url(),
                Owner = _faker.PickRandom(users)
            }).ToList();
        _dbContext.Shops.AddRange(shops);
        _dbContext.SaveChanges();
    }

    private void SeedCategories()
    {
        if (_dbContext.Categories.Any()) return;
        var categories = _faker.Commerce.Categories(_faker.Random.Number(5, 20)).Select(c => new Category
        {
            Name = c,
            Slug = _faker.Random.Word(),
            Details = _faker.Commerce.ProductMaterial(),
            Image = _faker.Image.PicsumUrl(),
            Icon = _faker.Image.PicsumUrl()
        }).ToList();
        _dbContext.Categories.AddRange(categories);
        _dbContext.SaveChanges();
    }

    private void SeedProducts()
    {
        if (_dbContext.Products.Any()) return;
        var shops = _dbContext.Shops.ToList();
        var categories = _dbContext.Categories.ToList();
        var products = Enumerable.Range(1, 30)
            .Select(i =>
            {
                return new Product
                {
                    Name = _faker.Company.CompanyName(),
                    Slug = _faker.Random.Word(),
                    Description = _faker.Company.CatchPhrase(),
                    ProductType = _faker.PickRandom<EProductType>(),
                    Status = _faker.PickRandom<EProductStatus>(),
                    InStock = true,
                    IsTaxable = true,
                    Price = double.Parse(_faker.Commerce.Price()),
                    Gallery = new[] { _faker.Image.PicsumUrl(), _faker.Image.PicsumUrl() },
                    Image = _faker.Image.PicsumUrl(),
                    Shop = _faker.PickRandom(shops),
                    Categories = categories.OrderBy(c => new Random().Next()).Take(3).ToList()
                };
            }).ToList();
        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }
}