using BookShop.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShop.Data
{
    public class BookShopContext : DbContext
    {
        public BookShopContext()
        {

        }

        public BookShopContext(DbContextOptions options)
            :base(options)
        {

        }

        public   DbSet<Book> Books { get; set; }
        public   DbSet<BookCategory> BookCategory { get; set; }
        public   DbSet<Category> Categories { get; set; }
        public   DbSet<Author> Authors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(Configuration.connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.ApplyConfiguration(new BookConfig());
            builder.ApplyConfiguration(new BookConfiguration());
            builder.ApplyConfiguration(new BookCategoryConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new AuthorConfiguration());
        }
    }
}
