using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        private void Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowVehiclesOptions(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new VehiclePanel());
        }

        public void SetMainPanel(UserControl userControl)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(userControl);
        }
    }
}
