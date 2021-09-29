using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class Format : INotifyPropertyChanged
    {
        private int _FormatId;
        private string _FormatName;
        private string _Description;

        public Format()
        {
            BookFormats = new HashSet<BookFormat>();
        }

        public int FormatId
        {
            get => _FormatId;
            set
            {
                _FormatId = value;
                RaisePropertyChanged(nameof(FormatId));
            }
        }
        public string FormatName
        {
            get => _FormatName;
            set
            {
                _FormatName = value;
                RaisePropertyChanged(nameof(FormatName));
            }
        }
        public string Description
        {
            get=>_Description;
            set
            {
                _Description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

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
