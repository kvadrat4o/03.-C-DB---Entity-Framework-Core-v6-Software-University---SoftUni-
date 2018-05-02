using BookShop.Data;
using BookShop.Initializer;
using BookShop.Models.Enums;
using System;
using System.Linq;

namespace BookShop.StartUp
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var db = new BookShopContext())
            {
                DbInitializer.ResetDatabase(db);
                var ageGroup = Console.ReadLine();
                Console.WriteLine(GetBooksByAgeRestriction(db,ageGroup));//1.	Age Restriction
                //Console.WriteLine(GetGoldenBooks(db));//2.	Golden Books
                //Console.WriteLine(GetGoldenBooks(db));//3.	Books by Price

            }
        }

        public static string  GetBooksByAgeRestriction (BookShopContext context, string command)
        {
            AgeRestriction parsed = (AgeRestriction)Enum.Parse(typeof(AgeRestriction), command, true);
            var books = context.Books.Where(b => b.AgeRestriction == parsed).OrderBy(b => b.Title).Select(b => b.Title).ToArray();
            return string.Join(Environment.NewLine, books);
        } //1.	Age Restriction

        //public static string GetGoldenBooks(BookShopContext context)
        //{
        //    var books = context.Books.Where(b => b.EditionType == EditionType.Gold && b.Copies < 5000).OrderBy(b => b.BookId).Select(b => b.Title).ToArray();
        //    return string.Join(Environment.NewLine, books);
        //} //2.	Golden Books

        //public static string GetBooksByPrice(BookShopContext context) //3.	Books by Price
        //{
               
        //}//3.	Books by Price
    }
}
