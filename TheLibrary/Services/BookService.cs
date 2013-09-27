using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLibrary.DAL;


namespace TheLibrary.Services
{
    class BookService
    {
        public IEnumerable<Book> GetAllBooks()
        {
            List<Book> result;
            using (var context = new TheLibraryEntities())
            {
                result = context.Books.Include("Author").ToList();
            }
            return result;
        }

        public IEnumerable<Book> GetAuthorBooks(string authorName)
        {
            List<Book> result;
            string firstWord = authorName;
            string secondWord = authorName;

            if (authorName.Contains(' '))
            {
                firstWord = authorName.Split(' ')[0];
                secondWord = authorName.Split(' ')[1];
                using (var context = new TheLibraryEntities())
                {
                    result = (from book in context.Books.Include("Author")
                              where (book.Author.Name.Contains(firstWord) && book.Author.Lastname.Contains(secondWord))
                              || (book.Author.Name.Contains(secondWord) && book.Author.Lastname.Contains(firstWord))
                              select book).ToList();
                }
            }
            else
            {
                using (var context = new TheLibraryEntities())
                {
                    result = (from book in context.Books.Include("Author")
                              where book.Author.Name.Contains(authorName) || book.Author.Lastname.Contains(authorName)
                              select book).ToList();
                }
            }

            return result;
        }

        public IEnumerable<Book> GetAverageRateBook(int year)
        {
            IEnumerable<Book> result;
            using (var context = new TheLibraryEntities())
            {
                int countBook = context.Books.Count();
                int countIssuance = context.BookIssuances.Count(c => c.IssuanceDate.Year == year);
                result = (from book in context.Books.Include("Author")
                               select new
                               {
                                   Book = book,
                                   Rate = (from rate in book.BookIssuances 
                                           select rate.IssuanceDate).Count()
                               }).Where(r => r.Rate > countIssuance / countBook && r.Rate != 0).Select(b => b.Book).ToList();
            }
            return result;
        }
    }
}
