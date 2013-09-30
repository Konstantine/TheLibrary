using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLibrary.DAL;


namespace TheLibrary.Services
{
    class UserService
    {
        private TheLibraryEntities _context = new TheLibraryEntities();

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public IEnumerable<User> GetBadUsers(DateTime date)
        {
            IEnumerable<User> result;
            result = (from user in _context.Users
                          where user.BookIssuances.Where(dt => dt.RequiredReturnDate < date && (!dt.RealReturnDate.HasValue || date < dt.RealReturnDate)).Count() > 0
                          select user).ToList();
            
            return result;
        }

        public IEnumerable<User> GetTopBadUsers(DateTime date)
        {
            IEnumerable<User> result;
            result = (from user in _context.Users
                      where user.BookIssuances.Where(dt => dt.RequiredReturnDate < date && (!dt.RealReturnDate.HasValue || date < dt.RealReturnDate)).Count() > 0
                      select new
                      {
                          User = user,
                          BadAss = user.BookIssuances.Where(dt => dt.RequiredReturnDate < date && (!dt.RealReturnDate.HasValue || date < dt.RealReturnDate)).Count()
                      }).OrderByDescending(b => b.BadAss).Select(u => u.User).Take(10).ToList();
            return result;
        }
    }
}
