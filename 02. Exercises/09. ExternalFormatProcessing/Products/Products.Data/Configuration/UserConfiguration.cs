using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Models;

namespace Products.Data
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.FirstName).IsRequired(false);
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.Age).IsRequired(false);

            builder.HasMany(e => e.ProductsBought).WithOne(p => p.Buyer).HasForeignKey(fk => fk.BuyerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(e => e.ProductsSold).WithOne(p => p.Seller).HasForeignKey(fk => fk.SellerId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}