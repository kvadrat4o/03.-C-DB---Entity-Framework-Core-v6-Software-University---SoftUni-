using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookShop.Data
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.AuthorId);

            builder.Property(a => a.FirstName).HasMaxLength(50).IsUnicode(true).IsRequired(false);
            builder.Property(a => a.LastName).HasMaxLength(50).IsUnicode(true).IsRequired(true);
        }
    }
}