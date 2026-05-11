using Bank2Pdf.ViewModels;
using System.Windows;

namespace Bank2Pdf.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(
            object sender,
            RoutedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            vm.RequestMinimize += HandleMinimize;
            vm.RequestToggleMaximize += HandleToggleMaximize;
            vm.RequestClose += HandleClose;
        }

        private void HandleMinimize()
        {
            WindowState = WindowState.Minimized;
        }

        private void HandleToggleMaximize()
        {
            WindowState =
                WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
        }

        private void HandleClose()
        {
            Close();
        }
    }
}