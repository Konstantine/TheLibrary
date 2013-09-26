using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLibrary.DAL
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
    }
}
