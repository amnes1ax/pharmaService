using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.PostgresSql.Extensions;
using PharmaService.DataAccess.PostgresSql.Repositories.Batches.Context.Configurations;
using PharmaService.DataAccess.PostgresSql.Repositories.Pharmacies.Context.Configurations;
using PharmaService.DataAccess.PostgresSql.Repositories.Products.Context.Configurations;
using PharmaService.DataAccess.PostgresSql.Repositories.Warehouses.Context.Configurations;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql;

public class PharmaDbContext : DbContext
{
    public PharmaDbContext(DbContextOptions<PharmaDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Model.SetDefaultSchema("public");
        modelBuilder.ApplyConfiguration(new PharmacyTypeConfiguration());
        modelBuilder.ApplyConfiguration(new WarehouseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BatchTypeConfiguration());
        modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Batch> Batches { get; set; } = null!;
    public DbSet<Pharmacy> Pharmacies { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
}