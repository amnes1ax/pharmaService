using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Products.Context.Configurations;

public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("product_id");
        builder.Property(m => m.Title)
            .HasColumnName("title");
        builder.Property(m => m.ShelfLife)
            .HasColumnName("shelf_life");
        
        builder
            .HasMany<Batch>()
            .WithOne(x=>x.Product)
            .HasForeignKey(ph => ph.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}