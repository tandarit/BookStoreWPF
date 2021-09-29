using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Book : INotifyPropertyChanged
    {
        private int _BookId;
        private string _BookName;
        private string _Description;
        private int? _PublisherId;
        private string _PictureUrl;
        private int _Price;

        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
            BookCategories = new HashSet<BookCategory>();
            BookFormats = new HashSet<BookFormat>();
        }

        public int BookId
        {
            get => _BookId;
            set
            {
                _BookId = value;
                RaisePropertyChanged(nameof(BookId));
            }
        }
        public string BookName
        {
            get => _BookName;
            set
            {
                _BookName = value;
                RaisePropertyChanged(nameof(BookName));
            }
        }
        public string Description
        {
            get => _Description;
            set
            {
                _Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }
        public int? PublisherId
        {
            get => _PublisherId;
            set
            {
                _PublisherId = value;
                RaisePropertyChanged(nameof(PublisherId));
            }
        }

        public string PictureUrl
        {
            get => _PictureUrl;
            set
            {
                _PictureUrl = value;
                RaisePropertyChanged(nameof(PictureUrl));
            }
        }
        public int Price
        {
            get => _Price;
            set
            {
                _Price = value;
                RaisePropertyChanged(nameof(Price));
            }
        }


        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
        public virtual ICollection<BookCategory> BookCategories { get; set; }
        public virtual ICollection<BookFormat> BookFormats { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
