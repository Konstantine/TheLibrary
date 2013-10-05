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
            IEnumerable<Book> result = new List<Book>();
            using (var context = new TheLibraryEntities())
            {
                int countBook = (from book in context.Books
                                 where book.BookIssuances.Where(i => i.IssuanceDate.Year == year).Count() > 0
                                 select book).Count();
                int countIssuance = context.BookIssuances.Where(c => c.IssuanceDate.Year == year).Count();
                if (countBook != 0)
                {
                    result = (from book in context.Books.Include("Author")
                              where book.BookIssuances.Where(i => i.IssuanceDate.Year == year).Count() >= countIssuance / countBook
                              select book).ToList();
                }                
            }
            return result;
        }

        public IEnumerable<Book> GetUndefinedBook(string year, string name)
        {
            IEnumerable<Book> result;
            int publish;
            if (int.TryParse(year, out publish))
            {
                using (var context = new TheLibraryEntities())
                {
                    result = (from book in context.Books
                              where (book.PublishDate.Value.Year == publish && book.Name.Contains(name))
                              select book).ToList();
                }
            }
            else
            {
                using (var context = new TheLibraryEntities())
                {
                    result = (from book in context.Books
                              where book.Name.Contains(name)
                              select book).ToList();
                }
            }
            return result;
        }
    }
}
