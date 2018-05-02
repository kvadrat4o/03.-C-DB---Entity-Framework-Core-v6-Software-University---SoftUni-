using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Models;

namespace Products.Data
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.BuyerId).IsRequired(false);
            builder.Property(p => p.Name).IsRequired(true);
            builder.Property(p => p.Price).IsRequired(true);
            builder.Property(p => p.SellerId).IsRequired(true);
        }
    }
}