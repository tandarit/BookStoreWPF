using BookStore.ViewModels;
using System.Windows;
using log4net;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly BookListViewModel _bookListViewModel;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public MainWindow(BookListViewModel bookListViewModel)
        {
            _bookListViewModel = bookListViewModel;
            DataContext = _bookListViewModel;
            log4net.Config.XmlConfigurator.Configure();

            InitializeComponent();
            
        }

    }
}
