using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowLoginPanel();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowVehiclesOptions(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new VehicleUserControl());
        }

        public void SetMainPanel(UserControl userControl)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(userControl);
        }

        private void LoginSucces()
        {
            WindowState = WindowState.Maximized;
            SetMainPanel(new MainPanel(LogOutAction));
        }

        private void LogOutAction()
        {
            ShowLoginPanel();
        }

        private void ShowLoginPanel()
        {
            WindowState = WindowState.Normal;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Width = 400;
            Height = 550;
            SetMainPanel(new LoginUserControl(LoginSucces));
        }
    }
}
