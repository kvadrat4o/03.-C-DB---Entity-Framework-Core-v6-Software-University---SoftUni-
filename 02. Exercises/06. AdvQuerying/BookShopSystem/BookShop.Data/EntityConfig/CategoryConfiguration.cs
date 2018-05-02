using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.CategoryId);

            builder.Property(c => c.Name).HasMaxLength(50).IsUnicode(true);

            builder.HasMany(c => c.CategoryBooks)
                .WithOne(cb => cb.Category)
                .HasForeignKey(fk => fk.CategoryId);
        }
    }
}