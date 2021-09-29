using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Author : INotifyPropertyChanged
    {
        private int _AuthorId;
        private string _AuthorFirstName;
        private string _AuthorLastName;
        private string _Description;

        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public int AuthorId 
        {
            get => _AuthorId;
            set {
                _AuthorId = value;
                RaisePropertyChanged(nameof(AuthorId));
            }
        }
        public string AuthorFirstName 
        { 
            get => _AuthorFirstName;
            set
            {
                _AuthorFirstName = value;
                RaisePropertyChanged(nameof(AuthorFirstName));
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
        public string AuthorLastName
        {
            get => _AuthorLastName;
            set
            {
                _AuthorLastName = value;
                RaisePropertyChanged(nameof(AuthorLastName));
            }
        }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }

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
