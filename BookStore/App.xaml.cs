using BookStore.Services;
using BookStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using log4net;
using BookStore.Commands;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// Scaffold-DbContext "Data Source=EPHUBUDW02FA;Initial Catalog=BookStoreDB;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        private static readonly ILog log = LogManager.GetLogger(typeof(App));

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<BookStoreDBContext>(options =>
            {
                options.UseSqlServer("Data Source=EPHUBUDW02FA;Initial Catalog=BookStoreDB;Integrated Security=True");
            });
            services.AddSingleton<BookListViewModel>();
            services.AddSingleton<IItemService, BookStoreItemService>();
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure();
            log.Info("        =============  Started Logging  =============        ");
            base.OnStartup(e);
        }
    }
}
