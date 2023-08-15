using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Batches.Context.Configurations;

public class BatchTypeConfiguration : IEntityTypeConfiguration<Batch>
{
    public void Configure(EntityTypeBuilder<Batch> builder)
    {
        builder.ToTable("batches");
        
        builder.HasKey(m => m.Id);
        
        builder.Property(m => m.Id)
            .HasColumnName("batch_id");
        builder.Property(m => m.ProductId)
            .HasColumnName("product_id");
        builder.Property(m => m.ProductCount)
            .HasColumnName("product_count");
        builder.Property(m => m.ArrivedOn)
            .HasColumnName("arrived_on");
        builder.Property(m => m.CreatedOn)
            .HasColumnName("created_on");
        builder.Property(m => m.ExpiredOn)
            .HasColumnName("expired_on");
        builder.Property(m => m.WarehouseId)
            .HasColumnName("warehouse_id");
        
        builder
            .HasOne(x=>x.Product)
            .WithMany()
            .HasForeignKey(b => b.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(x=>x.Warehouse)
            .WithMany(x=>x.Batches)
            .HasForeignKey(b => b.WarehouseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}