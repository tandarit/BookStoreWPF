using BookStore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// Scaffold-DbContext "Data Source=EPHUBUDW02FA;Initial Catalog=BookStoreDB;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
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
            services.AddSingleton<MainWindow>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
