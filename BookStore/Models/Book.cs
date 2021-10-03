using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Book 
    {      

        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
            BookCategories = new HashSet<BookCategory>();
            BookFormats = new HashSet<BookFormat>();
        }

        public int BookId
        {
            get;
            set;
            
        }
        public string BookName
        {
            get;
            set;
          
        }
        public string Description
        {
            get;
            set;
        }
        public int? PublisherId
        {
            get;
            set;
        }

        public string PictureUrl
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }


        public Publisher Publisher
        {
            get;
            set;
          
        }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<BookFormat> BookFormats { get; set; }

    }
}
