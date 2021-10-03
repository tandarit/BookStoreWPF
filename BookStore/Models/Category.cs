using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Category
    {  

        public Category()
        {
            BookCategories = new HashSet<BookCategory>();
        }

        public int CategoryId
        {
            get;
            set;
        }
        public string CategoryName
        {
            get;
            set;
        }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

    }
}
