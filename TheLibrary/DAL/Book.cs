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
    
    public partial class Book
    {
        public Book()
        {
            this.BookIssuances = new HashSet<BookIssuance>();
        }
    
        public int BookId { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
    
        public virtual Author Author { get; set; }
        public virtual ICollection<BookIssuance> BookIssuances { get; set; }
    }
}
