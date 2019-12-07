using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer;
using Entity;
using Entity.Common;

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

        private void RegisterRouteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(CostField.Text, out int cost))
                return;

            Route route = new Route(RouteCodeField.Text, OriginCityField.Text, DestinationCityField.Text, cost);

            RouteService routeService = new RouteService();

            if (routeService.Save(route))
                MessageBox.Show("Datos guardados");
            else
                MessageBox.Show("Error al guardar los datos");
        }

        private void HandleServerAnswer(ServerAnswer answer)
        {
            if (answer == ServerAnswer.DATA_ALREADY_REGISTERED)
                MessageBox.Show("Datos ya registrados");
            else if (answer == ServerAnswer.SAVE_SUCCESSFUL)
                MessageBox.Show("Datos registrados con exito");
        }
    }
}
