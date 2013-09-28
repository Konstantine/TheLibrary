using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLibrary.DAL;

namespace TheLibrary.Services
{
    class BookIssuanceService
    {
        private TheLibraryEntities _context = new TheLibraryEntities();

        public IEnumerable<BookIssuance> GetAllIssuance()
        {
            return _context.BookIssuances.Include("User").Include("Book").ToList();
        }

        public bool GiveBook(int userId, int bookId, DateTime requredReturnDate)
        {            
            var newIssuance = new BookIssuance
            {
                UserId = userId,
                BookId = bookId,
                RequiredReturnDate = requredReturnDate,
                IssuanceDate = DateTime.Now
            };
            if (!_context.Users.Single(u => u.UserId == userId).IsBanned)
            {
                _context.BookIssuances.Add(newIssuance);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void ReturnBook(BookIssuance issuance)
        {
            if(!issuance.RealReturnDate.HasValue)
            {
                issuance.RealReturnDate = DateTime.Now;
                _context.SaveChanges();
            }
        }
    }
}
