using System.ComponentModel;

#nullable disable

namespace BookStore.Models
{
    public partial class BookAuthor : INotifyPropertyChanged
    {
        private int _BookAuthorId;

        public int BookAuthorId
        { 
            get => _BookAuthorId; 
            set
            {
                _BookAuthorId = value;
                RaisePropertyChanged(nameof(BookAuthorId));
            } 
        }
        public int BookId { get; set; }
        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }

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
