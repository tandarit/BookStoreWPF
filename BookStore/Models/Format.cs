using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Format
    {
        public Format()
        {
            BookFormats = new HashSet<BookFormat>();
        }

        public int FormatId
        {
            get;
            set;
        }
        public string FormatName
        {
            get;
            set;
           
        }
        public string Description
        {
            get;
            set;
        
        }

        public virtual ICollection<BookFormat> BookFormats { get; set; }
       
    }
}
