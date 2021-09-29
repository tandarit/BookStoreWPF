using BookStore.Models;
using System.Collections.ObjectModel;


namespace BookStore.ViewModels
{
    class BookListViewModel
    {
        private readonly BookStoreDBContext _databaseContext;
        private Book _selectedBook;
        private ObservableCollection<Book> _books;

        public BookListViewModel(BookStoreDBContext databaseContext)
        {
            _databaseContext = databaseContext;
            _books = new ObservableCollection<Book>(_databaseContext.Books);
            
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            AddCommand = new MyICommand(OnAdd, CanAdd);
        }

        public MyICommand DeleteCommand { get; set; }
        public MyICommand AddCommand { get; set; }

        private void OnAdd()
        {
            
        }

        private bool CanAdd()
        {
            return true;
        }


        private void OnDelete()
        {

        }

        private bool CanDelete()
        {
            return SelectedBook != null;
        }

        public Book SelectedBook
        {
            get
            {
                return _selectedBook;
            }

            set
            {
                _selectedBook = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Book> Books
        {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
            }
        }


    }
}
