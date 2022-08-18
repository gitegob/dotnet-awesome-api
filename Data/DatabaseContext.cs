using Dotnet_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_API.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }


    public DbSet<User>? Users { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductVariation>? ProductsVariations { get; set; }
    public DbSet<Shop>? Shops { get; set; }
    public DbSet<Category>? Categories { get; set; }

    public override int SaveChanges()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Deleted) continue;
            entry.State = EntityState.Modified;
            var entity = entry.Entity;
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }

        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Deleted) continue;
            entry.State = EntityState.Modified;
            var entity = entry.Entity;
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }

        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Deleted) continue;
            entry.State = EntityState.Modified;
            var entity = entry.Entity;
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = new())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.State != EntityState.Deleted) continue;
            entry.State = EntityState.Modified;
            var entity = entry.Entity;
            entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
        }

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
}