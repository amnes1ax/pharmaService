using Microsoft.EntityFrameworkCore;
using PharmaService.DataAccess.PostgresSql.Extensions;
using PharmaService.DataAccess.PostgresSql.Repositories.Warehouses.Context.Configurations;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Warehouses.Context;

public class WarehouseDbContext : DbContext
{
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Model.SetDefaultSchema("public");
        modelBuilder.ApplyConfiguration(new WarehouseTypeConfiguration());
        modelBuilder.ApplyUtcDateTimeConverter();
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Warehouse> Warehouses { get; set; }
}