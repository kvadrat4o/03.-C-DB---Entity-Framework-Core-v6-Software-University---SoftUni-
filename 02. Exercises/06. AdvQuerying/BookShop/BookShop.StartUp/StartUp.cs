namespace BookShop
{
    using BookShop.Data;
    using BookShop.Initializer;
    using BookShop.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new BookShopContext())
            {
                var ageGroup = Console.ReadLine();
                Console.WriteLine(GetBooksByAgeRestriction(db, ageGroup));//1.	Age Restriction
                Console.WriteLine(GetGoldenBooks(db));//2.	Golden Books
                Console.WriteLine(GetBooksByPrice(db));//3.	Books by Price
                var year = int.Parse(Console.ReadLine());
                Console.WriteLine(GetBooksNotRealeasedIn(db, year));//4.	Not Released In
                var input = Console.ReadLine();
                Console.WriteLine(GetBooksByCategory(db, input));//5.	Book Titles by Category
                string date = Console.ReadLine();
                Console.WriteLine(GetBooksReleasedBefore(db, date));//6.	Released Before Date
                string input1 = Console.ReadLine();
                Console.WriteLine(GetAuthorNamesEndingIn(db, input1));//7.	Author Search
                string input2 = Console.ReadLine();
                Console.WriteLine(GetBookTitlesContaining(db, input2));//8.	Book Search
                string input3 = Console.ReadLine();
                Console.WriteLine(GetBooksByAuthor(db, input3));//9.	Book Search by Author
                int lengthCheck = int.Parse(Console.ReadLine());
                Console.WriteLine($"There are {CountBooks(db, lengthCheck)} books with longer title than {lengthCheck} symbols");//10.	Count Books
                Console.WriteLine(CountCopiesByAuthor(db));//11.	Total Book Copies
                Console.WriteLine(GetTotalProfitByCategory(db));//12.	Profit by Category
                Console.WriteLine(GetMostRecentBooks(db));//13.	Most Recent Books
                Console.WriteLine(IncreasePrices(db));//14.	Increase Prices
                Console.WriteLine(RemoveBooks(db));//15.	Remove Books
                DbInitializer.ResetDatabase(db);
            }
        }

        public static int RemoveBooks(BookShopContext db, int copies = 4200)
        {
            var books = db.Books
                    .Where(b => b.Copies < copies)
                    .ToArray();

            var removed = books.Length;

            db.Books.RemoveRange(books);
            db.SaveChanges();

            return removed;
        }

        //14.	Increase Prices
        public static int IncreasePrices(BookShopContext db, decimal increasement = 5, int year = 2010)
        {
            var books = db.Books
                .Where(b => b.ReleaseDate != null && b.ReleaseDate.Value.Year < year)
                .ToArray();

            foreach (var book in books)
            {
                book.Price += increasement;
            }

            return db.SaveChanges();
        }

        //13.	Most Recent Books
        public static string GetMostRecentBooks(BookShopContext db)
        {
            return "--" + string.Join($"{Environment.NewLine}--", db.Categories
                .Select(c => new
                {
                    Name = c.Name,
                    BookCount = c.CategoryBooks
                        .Select(cb => cb.Book)
                        .Count(),
                    TopThreeString = string.Join(Environment.NewLine, c.CategoryBooks
                        .Select(cb => cb.Book)
                        .OrderByDescending(b => b.ReleaseDate)
                        .Take(3)
                        .Select(b => b.ReleaseDate == null
                            ? $"{b.Title} ()"
                            : $"{b.Title} ({b.ReleaseDate.Value.Year})"))
                })
                // .OrderBy(c => c.BookCount) // Wrong Requirement - Judge wants sorting by Name
                .OrderBy(c => c.Name)
                .Select(c => $"{c.Name}{Environment.NewLine}{c.TopThreeString}"));
        }

        //12.	Profit by Category
        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var profits = db.Categories.Select(c => new { c.Name, profit = c.CategoryBooks.Select(cb => cb.Book.Copies * cb.Book.Price).Sum() }).OrderByDescending(c => c.profit).ThenBy(c => c.Name).Select(c => $"{c.Name} ${c.profit:F2}").ToArray();
            return string.Join(Environment.NewLine, profits);
        }

        //11.	Total Book Copies
        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var authors = db.Authors.Include(x => x.Books).ToList();

            var result = new StringBuilder();

            foreach (var a in authors.OrderByDescending(a => a.Books.Select(au => au.Copies).Sum()))
            {
                result.AppendLine($"{a.FirstName} {a.LastName} - {a.Books.Select(x => x.Copies).Sum()}");
            }

            return result.ToString().Trim();
        }

        //10.	Count Books
        public static int CountBooks(BookShopContext db, int lengthCheck)
        {
            var number = db.Books.Where(b => b.Title.Length > lengthCheck).OrderBy(b => b.BookId).Select(b => b.Title).ToArray().Count();
            return number;
        }

        //9.	Book Search by Author
        public static string GetBooksByAuthor(BookShopContext db, string input)
        {
            input = input.ToLower();
            var books = db.Books.Where(b => b.Author.LastName.ToLower().StartsWith(input)).OrderBy(b => b.BookId).Select(b => $"{b.Title} ({b.Author.FirstName} {b.Author.LastName})");
            return string.Join(Environment.NewLine, books);
        }

        //8.	Book Search
        public static string GetBookTitlesContaining(BookShopContext db, string input)
        {
            if (input == null)
            {
                input = Console.ReadLine();
            }

            input = input.ToLower();

            return string.Join(Environment.NewLine, db.Books.Where(b => b.Title.ToLower().Contains(input)).Select(b => b.Title).OrderBy(t => t));
        }

        ////7.	Author Search
        public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        {
            var authors = db.Authors.Where(a => a.FirstName != null && a.FirstName.EndsWith(input))
                .Select(a => a.FirstName == null ? a.LastName : $"{a.FirstName} {a.LastName}")
                .OrderBy(n => n).ToArray();
            return string.Join(Environment.NewLine, authors);
        }

        //6.	Released Before Date
        public static string GetBooksReleasedBefore(BookShopContext db, string date)
        {
            var date1 = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InstalledUICulture);
            var books = db.Books.Where(b => b.ReleaseDate < date1).OrderByDescending(b => b.ReleaseDate).Select(b => new { title = b.Title, edition = b.EditionType, price = b.Price }).ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (var book in books)
            {
                sb.AppendLine($"{book.title} - {book.edition} - ${book.price:f2}");
            }
            return sb.ToString();
        }

        //5.	Book Titles by Category
        public static string GetBooksByCategory(BookShopContext db, string input)
        {
            if (input == null)
            {
                input = Console.ReadLine();
            }
            var categories = input.ToLower().Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var books = db.Books.Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower()))).OrderBy(b => b.Title).Select(b => b.Title).ToArray();
            return string.Join(Environment.NewLine, books);
        }

        //4.	Not Released In
        public static string GetBooksNotRealeasedIn(BookShopContext db, int year)
        {
            var books = db.Books.Where(bk => bk.ReleaseDate.Value.Year != year).OrderBy(bk => bk.BookId).Select(bk => bk.Title).ToArray();
            //string result = "";
            return String.Join(Environment.NewLine, books);
        }

        //3.	Books by Price
        public static string GetBooksByPrice(BookShopContext context) //3.	Books by Price
        {
            var books = context.Books.Where(bk => bk.Price > 40).OrderByDescending(ba => ba.Price).Select(bb => new { title = bb.Title, price = bb.Price }).ToList();
            var b = new StringBuilder();
            foreach (var book in books)
            {
                b.AppendLine($"{book.title} - ${book.price:f2}");
            }
            return b.ToString();
        }

        //2.	Golden Books
        public static string GetGoldenBooks(BookShopContext context)
        {
            var books = context.Books.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000).OrderBy(b => b.BookId).Select(b => b.Title).ToArray();
            return string.Join(Environment.NewLine, books);
        }

        //1.	Age Restriction
        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            AgeRestriction parsed = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);
            var books = context.Books.Where(b => b.AgeRestriction == parsed).OrderBy(b => b.Title).Select(b => b.Title).ToArray();
            return string.Join(Environment.NewLine, books);
        }
    }
}
