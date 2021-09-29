using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Publisher : INotifyPropertyChanged
    {
        private int _PublisherId;
        private string _PublisherName;

        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public int PublisherId
        {
            get => _PublisherId;
            set
            {
                _PublisherId = value;
                RaisePropertyChanged(nameof(PublisherId));
            }
        }
        public string PublisherName 
        { 
            get=>_PublisherName; 
            set
            {
                _PublisherName = value;
                RaisePropertyChanged(nameof(PublisherName));
            }
        }

        public virtual ICollection<Book> Books { get; set; }

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
