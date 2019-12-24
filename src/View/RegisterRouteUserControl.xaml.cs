using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterRouteUserControl.xaml
    /// </summary>
    public partial class RegisterRouteUserControl : UserControl
    {
        public RegisterRouteUserControl()
        {
            InitializeComponent();
        }

        private async void RegisterRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CostField.Text, out int cost))
            {
                MessageBox.Show("Costo invalido", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Route route = new Route(RouteCodeField.Text, OriginCityField.Text, DestinationCityField.Text, cost);

            if (await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.ROUTE, route))
                HandleServerAnswer();
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
                MessageBox.Show("Datos registrados con exito");
            else
                MessageBox.Show("Datos ya registrados");

        }
    }
}
