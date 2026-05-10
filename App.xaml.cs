using Bank2Pdf.ViewModels;
using Bank2Pdf.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Bank2Pdf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<MainWindow>();

            Services = serviceCollection.BuildServiceProvider();

            var window = Services.GetRequiredService<MainWindow>();

            window.DataContext =
                Services.GetRequiredService<MainViewModel>();

            window.Show();
        }
    }

}
