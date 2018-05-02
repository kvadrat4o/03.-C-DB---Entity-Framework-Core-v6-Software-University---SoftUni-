using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BookShop.Data
{
    public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder.HasOne(c => c.Category)
                .WithMany(cb => cb.CategoryBooks)
                .HasForeignKey(fk => fk.CategoryId);

            builder.HasOne(b => b.Book)
                .WithMany(bc => bc.BookCategories)
                .HasForeignKey(fk => fk.BookId);
        }
    }
}