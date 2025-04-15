using Ambev.Domain.Entities.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.Infrastructure.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {

            builder.HasKey(s => s.Id);

            builder.HasIndex(s => s.SaleNumber)
                .IsUnique();

            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(s => s.ShippingAddress, a =>
            {
                a.Property(addr => addr.City).HasMaxLength(100);
                a.Property(addr => addr.Street).HasMaxLength(200);
                a.Property(addr => addr.Number).HasMaxLength(100);
                a.Property(addr => addr.ZipCode).HasMaxLength(20);
                a.Property(addr => addr.Latitude).HasMaxLength(50);
                a.Property(addr => addr.Longitude).HasMaxLength(50);
                a.WithOwner();
            });
        }
    }
}
