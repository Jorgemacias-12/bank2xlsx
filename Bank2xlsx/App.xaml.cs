using Bank2xlsx.ViewModels;
using Bank2xlsx.Views;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace Bank2xlsx
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
