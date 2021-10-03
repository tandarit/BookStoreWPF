using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int AuthorId
        {
            get;
            set;
        }
        public string AuthorFirstName
        {
            get;
            set;
        }
        public string Description
        {
            get;
            set;
        }
        public string AuthorLastName
        {
            get; set;
        }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        
    }
}
