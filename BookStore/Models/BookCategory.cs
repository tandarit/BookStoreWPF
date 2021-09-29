using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models
{
    public partial class BookCategory
    {
        public int BookCategory1 { get; set; }
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Category Category { get; set; }
    }
}
