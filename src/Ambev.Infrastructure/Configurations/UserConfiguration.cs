using Ambev.Shared.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).HasMaxLength(255).IsRequired();

            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();

            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(u => u.Phone).HasMaxLength(20);

            builder.OwnsOne(u => u.Name, n =>
            {
                n.Property(name => name.FirstName).HasMaxLength(100);
                n.Property(name => name.LastName).HasMaxLength(100);
                n.WithOwner();
            });

            builder.OwnsOne(u => u.Address, a =>
            {
                a.Property(addr => addr.City).HasMaxLength(100).IsRequired();
                a.Property(addr => addr.Street).HasMaxLength(200).IsRequired();
                a.Property(addr => addr.Number).IsRequired();
                a.Property(addr => addr.ZipCode).HasMaxLength(20).IsRequired();
                a.Property(addr => addr.Latitude).HasMaxLength(50);
                a.Property(addr => addr.Longitude).HasMaxLength(50);
                a.WithOwner();
            });
        }
    }
}
