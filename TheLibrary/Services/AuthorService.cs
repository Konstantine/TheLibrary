using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLibrary.DAL;

namespace TheLibrary.Services
{
    class AuthorService
    {
        public IEnumerable<Author> GetAllAuthors()
        {
            List<Author> result;
            using (var context = new TheLibraryEntities())
            {
                result = context.Authors.ToList();
            }
            return result;
        }

        public IEnumerable<Author> GetPopularInYear(int year)
        {
            IEnumerable<Author> result;
            using (var context = new TheLibraryEntities())
            {
                result = (from author in context.Authors

                          select new
                          {
                              Author = author,
                              PopularityIndex = (from book in author.Books
                                                 select (from issuance in book.BookIssuances
                                                         where issuance.IssuanceDate.Year == year
                                                         select issuance).Count()
                                                 ).Sum()
                          }).Where(a => a.PopularityIndex > 0)
                          .OrderByDescending(a => a.PopularityIndex)
                          .Select(a => a.Author).ToList();
            }
            return result;

        }
    }
}
