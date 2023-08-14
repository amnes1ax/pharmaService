using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PharmaService.Domain.Entities;

namespace PharmaService.DataAccess.PostgresSql.Repositories.Pharmacies.Context.Configurations;

public class PharmacyTypeConfiguration : IEntityTypeConfiguration<Pharmacy>
{
    public void Configure(EntityTypeBuilder<Pharmacy> builder)
    {
        builder.ToTable("pharmacies");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasColumnName("pharmacy_id");
        builder.Property(m => m.Title)
            .HasColumnName("title");
        builder.Property(m => m.Address)
            .HasColumnName("address");
        builder.Property(m => m.PhoneNumber)
            .HasColumnName("phone_number");

        builder.HasMany<Warehouse>()
            .WithOne()
            .HasForeignKey(ph => ph.PharmacyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}