using BookStore.ViewModels;
using System.Windows;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BookListViewModel _bookListViewModel;
        
        public MainWindow(BookListViewModel bookListViewModel)
        {
            _bookListViewModel = bookListViewModel;
            DataContext = _bookListViewModel;
           
            InitializeComponent();
            
        }

    }
}
