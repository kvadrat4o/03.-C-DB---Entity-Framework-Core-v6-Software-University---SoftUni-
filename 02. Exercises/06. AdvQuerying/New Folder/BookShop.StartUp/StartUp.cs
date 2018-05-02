using System;
using System.Linq;
using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);

                var input = Console.ReadLine().ToLower();
                var output = GetBooksByAgeRestriction(db, input);

                Console.WriteLine(output);
            }
        }

        public static string GetBooksByAgeRestriction(BookShopContext db, string command)
        {
            AgeRestriction parsed = (AgeRestriction)Enum.Parse(typeof(AgeRestriction),command,true);
            
            var books = db.Books
                .Where(x => x.AgeRestriction == parsed)
                .OrderBy(x => x.Title)
                .Select(x => x.Title)
                .ToList();

            return string.Join(Environment.NewLine, books);
        }
    }
}
