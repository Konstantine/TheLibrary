//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheLibrary.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.BookIssuances = new HashSet<BookIssuance>();
        }
    
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public bool IsBanned { get; set; }
    
        public virtual ICollection<BookIssuance> BookIssuances { get; set; }
    }
}
