using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Category : INotifyPropertyChanged
    {
        private int _CategoryId;
        private string _CategoryName;

        public Category()
        {
            BookCategories = new HashSet<BookCategory>();
        }

        public int CategoryId
        {
            get => _CategoryId;
            set
            {
                _CategoryId = value;
                RaisePropertyChanged(nameof(CategoryId));
            }
        }
        public string CategoryName
        {
            get => _CategoryName;
            set
            {
                _CategoryName = value;
                RaisePropertyChanged(nameof(CategoryName));
            }
        }

        public virtual ICollection<BookCategory> BookCategories { get; set; }

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
