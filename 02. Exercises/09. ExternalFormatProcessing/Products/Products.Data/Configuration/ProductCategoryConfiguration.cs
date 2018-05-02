using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Products.Models;

namespace Products.Data
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => new { pc.CategoryId, pc.ProductId });

            builder.HasOne(pc => pc.Product).WithMany(p => p.Categories).HasForeignKey(fk => fk.ProductId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(pc => pc.Category).WithMany(c => c.Products).HasForeignKey(fk => fk.CategoryId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}