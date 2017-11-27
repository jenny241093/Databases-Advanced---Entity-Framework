using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
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
                // var input = int.Parse(Console.ReadLine());
                // DbInitializer.ResetDatabase(db);

                //-----1.Age Restriction
                //var restriction = Console.ReadLine();
                //Console.WriteLine(GetBooksByAgeRestriction(db, restriction));


                //-----2.Golden Books
                // Console.WriteLine(GetGoldenBooks(db));


                //-----3.Books by Price
                // Console.WriteLine(GetBooksByPrice(db));

                //-----4.Not Released In
                //Write a GetBooksNotRealeasedIn(BookShopContext context, int year) method that returns in a single string all titles of books that are NOT released on a given year.Order them by book id ascending.
                //var year = int.Parse(Console.ReadLine());
                //Console.WriteLine(GetBooksNotRealeasedIn(db, year));


                //-----5.Book Titles by Category
                //    Write a GetBooksByCategory(BookShopContext context, string input) method that selects and returns in a single string the titles of books by a given list of categories.The list of categories will be given in a single line separated with one or more spaces.Ignore casing. Order by title alphabetically.
                //var category = Console.ReadLine();
                //Console.WriteLine(GetBooksByCategory(db,category));

                //-----6.Released Before Date
                //Write a GetBooksReleasedBefore(BookShopContext context, string date) method that returns the title, edition type and price of all books that are released before a given date. The date will be a string in format dd-MM - yyyy.
                //    Return all of the rows in a single string, ordered by release date descending.
                //var date =Console.ReadLine();
                //Console.WriteLine(GetBooksReleasedBefore(db, date));
                //-----7.Author Search
                //    Write a GetAuthorNamesEndingIn(BookShopContext context, string input) method that returns the full names of authors, whose first name ends with a given string.
                //    Return all names in a single string, each on a new row, ordered alphabetically.
                //string input = Console.ReadLine();
                //Console.WriteLine(GetAuthorNamesEndingIn(db, input));

                //-----8.Book Search
                //Write a GetBookTitlesContaining(BookShopContext context, string input) method that returns the titles of book, which contain a given string.Ignore casing.
                //    Return all titles in a single string, each on a new row, ordered alphabetically.
                //Console.WriteLine(GetBookTitlesContaining(db, input));

                //-----9.Book Search by Author
                //    Write a GetBooksByAuthor(BookShopContext context, string input) method that returns all titles of books and their authors’ names for books, which are written by authors whose last names start with the given string.
                //Return a single string with each title on a new row.Ignore casing.Order by book id ascending.               
                //Console.WriteLine(GetBooksByAuthor(db,input));

                //-----10.Count Books
                //Console.WriteLine(CountBooks(db,input));


                //-----11.Total Book Copies
                //   Console.WriteLine(CountCopiesByAuthor(db));
                //-----12.Profit by Category
                //Console.WriteLine(GetTotalProfitByCategory(db));

                //13.Most Recent Books
                //Get the most recent books by categories in a GetMostRecentBooks(BookShopContext context) method.The categories should be ordered by total book count.Only take the top 3 most recent books from each category -ordered by release date(descending).Select and print the category name, and for each book – its title and release year.
                //Console.WriteLine(GetMostRecentBooks(db));
                //14.Increase Prices
                //    Write a method IncreasePrices(BookShopContext context) that increases the prices of all books released before 2010 by 5.
                //IncreasePrices(db);
                //15.Remove Books
                //    Write a method RemoveBooks(BookShopContext context) that removes from the database those books, which have less than 4200 copies.Return an int -the number of books that were deleted from the database.

                Console.WriteLine($"{RemoveBooks(db)} books were deleted");
                
            }

        }

        public static int RemoveBooks(BookShopContext context)
        {
            var books = context.Books.Where(e => e.Copies < 4000).ToList();
            context.Books.RemoveRange(books);
            context.SaveChanges();
            var count = books.Count;
            return count;
        }

        public static void IncreasePrices(BookShopContext context)
        {
            var books = context.Books.Where(e => e.ReleaseDate.Value.Year < 2010).ToList();
            books.ForEach(e => e.Price += 5);
        }

        public static string GetMostRecentBooks(BookShopContext db)
        {
            var categories = db.Categories.Include(b => b.CategoryBooks)
                                .ThenInclude(cb => cb.Book).ToList();

            var result = new StringBuilder();
            foreach (var category in categories.OrderBy(c => c.Name))
            {
                result.AppendLine($"--{category.Name}");
                foreach (var cb in category.CategoryBooks.OrderByDescending(cb => cb.Book.ReleaseDate).Take(3))
                {
                    result.AppendLine($"{cb.Book.Title} ({cb.Book.ReleaseDate.Value.Year})");
                }
            }
            return result.ToString();
        }

        public static string GetTotalProfitByCategory(BookShopContext db)
        {
            var profit = db.Categories.Select(e => new
            {
                Category = e.Name,
                Profit = e.CategoryBooks.Sum(cb => cb.Book.Copies * cb.Book.Price)
            })
            .OrderByDescending(x => x.Profit)
            .ThenBy(x => x.Category)
            .ToList();
            var sb = new StringBuilder();
            profit.ForEach(c => sb.AppendLine($"{c.Category} ${c.Profit:f2}"));
            return sb.ToString().Trim();
        }

        public static string CountCopiesByAuthor(BookShopContext db)
        {
            var books = db
                .Authors
                
                .Select(a => new
                {
                    Name = $"{a.FirstName} {a.LastName}",
                    Copies = a.Books.Sum(b => b.Copies)
                })
                .OrderByDescending(x => x.Copies)
                .ToList();

            var result = new StringBuilder();

            books.ForEach(x => result.AppendLine($"{x.Name} - {x.Copies}"));

            return result.ToString();
           
        }

        public static int CountBooks(BookShopContext db,int input)
        {
            var count = db.Books.Where(e => e.Title.Length > input).ToList().Count;
            return count;
        }

        public static string GetBooksByAuthor(BookShopContext db, string input)
        {
            var books = db.Books.Where(e => e.Author.LastName.ToLower().StartsWith(input.ToLower())).OrderBy(e => e.BookId)
                .Select(e => $"{e.Title} ({e.Author.FirstName} {e.Author.LastName})").ToList();
            var sb = new StringBuilder();
            books.ForEach(e => sb.AppendLine($"{e}"));
            return sb.ToString();
        }

        public static string GetBookTitlesContaining(BookShopContext db, string input)
        {
            var sb = new StringBuilder();
            var books = db.Books.Where(e => e.Title.ToLower().Contains(input.ToLower())).OrderBy(e => e.Title)
                .ToList();
            books.ForEach(e => sb.AppendLine($"{e.Title}"));
           
            return sb.ToString().Trim();
        }

        public static string GetAuthorNamesEndingIn(BookShopContext db, string input)
        {
            var authors = db.Authors.Where(e => e.FirstName.EndsWith(input)).OrderBy(e => e.FirstName + " "+e.LastName)
                                .ToList();
            if (!authors.Any())
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            //authors.ForEach(e => sb.AppendLine($"{e.Author.FirstName} {e.Author.LastName}"));
            foreach (var author in authors)
            {
                sb.AppendLine($"{author.FirstName} {author.LastName}");
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksReleasedBefore(BookShopContext db,string date)
        {
           var checkDate = DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
            var booksReleasedBefore = db.Books.Where(e => e.ReleaseDate < checkDate).ToList();
            if (!booksReleasedBefore.Any())
            {
                return string.Empty;
            }
            var sb = new StringBuilder();
            foreach (var book in booksReleasedBefore.OrderByDescending(e => e.ReleaseDate))
            {
                sb.AppendLine($"{book.Title} - {book.EditionType} - ${book.Price:f2}");
            }
            return sb.ToString().Trim();
        }
    

        public static string GetBooksByCategory(BookShopContext context, string input)
        {
            string[] categories = input.ToLower().Split(new[] {"\t", " ", Environment.NewLine},
                StringSplitOptions.RemoveEmptyEntries);

            var titles = context.Books.Where(b => b.BookCategories.Any(c => categories.Contains(c.Category.Name.ToLower())))
                .Select(b => b.Title)
                .OrderBy(t => t).ToList();

            string result = string.Join(Environment.NewLine, titles);
            return result;
        }

        
        public static string GetBooksNotRealeasedIn(BookShopContext db,int year)
        {
            
            var sb = new StringBuilder();
            var booksNotRealisedIn = db.Books.Where(e => e.ReleaseDate.Value.Year != year).ToList();
            foreach (var book in booksNotRealisedIn.OrderBy(e => e.BookId))
            {
                sb.AppendLine($"{book.Title}");
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksByPrice(BookShopContext db)
        {
            var booksWithPriceHigherThan40 = db.Books.Where(e => e.Price > 40).ToList();

            var sb = new StringBuilder();
            foreach (var book in booksWithPriceHigherThan40.OrderByDescending(e => e.Price))
            {
                sb.AppendLine($"{book.Title} - ${book.Price:f2}");
            }
            return sb.ToString().Trim();
        }

        public static string GetBooksByAgeRestriction(BookShopContext context, string command)
        {
            var sb = new StringBuilder();
            int dbValue = 0;
            switch (command.ToLower())
            {
                case "minor":
                    dbValue = 0;
                    break;
                case "teen":
                    dbValue = 1;
                    break;
                case "adult":
                    dbValue = 2;
                    break;
            }
            var books = context.Books.Where(e => (int)e.AgeRestriction == dbValue).ToList();
            foreach (var book in books.OrderBy(e => e.Title))
            {
                sb.AppendLine(book.Title);
            }
            return sb.ToString().Trim();
        }

        public static string GetGoldenBooks(BookShopContext context)
        {
            var sb = new StringBuilder();
            var goldenBooks = context.Books.Where(e => e.Copies < 5000 && (int)e.EditionType == 2).ToList();
            foreach (var goldenBook in goldenBooks.OrderBy(e => e.BookId))
            {
                sb.AppendLine(goldenBook.Title);
            }
            return sb.ToString().Trim();
        }

    }
}
