using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BookShop.Data
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Description).HasMaxLength(1000).IsUnicode(true).IsRequired(true);
            builder.Property(b => b.Copies).IsRequired(true);
            builder.Property(b => b.ReleaseDate).IsRequired(false);
            builder.Property(b => b.Price).IsRequired(true);
            builder.Property(b => b.EditionType).IsRequired(true);
            builder.Property(b => b.AgeRestriction).IsRequired(true);
            builder.Property(b => b.Title).HasMaxLength(50).IsUnicode(true).IsRequired(true);

            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(fk => fk.AuthorId);
        }
    }
}