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
        public IEnumerable<User> GetAllUsers()
        {
            IEnumerable<User> result;
            using (var context = new TheLibraryEntities())
            {
                result = (from user in context.Users
                          select user).ToList();
            }
            return result;
        }

        //public IEnumerable<User> UserIsBanned()
        //{
        //    using (var context = new TheLibraryEntities())
        //    {
 
        //    }
        //}
    }
}
