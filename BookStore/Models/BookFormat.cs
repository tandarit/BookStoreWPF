using System;
using System.Collections.Generic;

#nullable disable

namespace BookStore.Models
{
    public partial class BookFormat
    {
        public int BookFormatId { get; set; }
        public int BookId { get; set; }
        public int FormatId { get; set; }

        public virtual Book Book { get; set; }
        public virtual Format Format { get; set; }
    }
}
