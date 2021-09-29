using BookStore.ViewModels;
using System.Windows;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BookStoreDBContext _bookStoreDBContext;

        public MainWindow(BookStoreDBContext bookStoreDBContext)
        {
            _bookStoreDBContext = bookStoreDBContext;
            InitializeComponent();
        }

    }
}
