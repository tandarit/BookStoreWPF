using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BookStore.Models
{
    public class BookPresenterModel : INotifyPropertyChanged
    {
        private int _BookId;
        private string _BookName;
        private string _Description;
        private string _PictureUrl;
        private int _Price;
        private Publisher _Publisher;

        public BookPresenterModel()
        {
            BookAuthors = new ObservableCollection<Author>();
            BookCategories = new ObservableCollection<Category>();
            BookFormats = new ObservableCollection<Format>();
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
        public Publisher Publisher
        {
            get => _Publisher;
            set
            {
                _Publisher = value;
                RaisePropertyChanged(nameof(Publisher));
            }
        }
        public ObservableCollection<Author> BookAuthors { get; set; }
        public ObservableCollection<Category> BookCategories { get; set; }
        public ObservableCollection<Format> BookFormats { get; set; }

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
