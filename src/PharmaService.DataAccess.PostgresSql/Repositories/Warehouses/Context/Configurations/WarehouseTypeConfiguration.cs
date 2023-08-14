using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Warehouses.Context.Configurations;

public class WarehouseTypeConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("warehouses");
        
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Id)
            .HasColumnName("warehouse_id");
        builder.Property(m => m.Title)
            .HasColumnName("title");
        builder.Property(m => m.PharmacyId)
            .HasColumnName("pharmacy_id");
        
        builder.HasOne<Pharmacy>()
            .WithMany()
            .HasForeignKey(w => w.PharmacyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}