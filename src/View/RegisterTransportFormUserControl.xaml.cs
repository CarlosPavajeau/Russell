using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BusinessLogicLayer;
using Entity;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterTransportFormUserControl.xaml
    /// </summary>
    public partial class RegisterTransportFormUserControl : UserControl
    {
        public RegisterTransportFormUserControl()
        {
            InitializeComponent();

            TransportFormDate.Text += DateTime.Now.ToShortDateString();
            TransportFormDispatcher.Text += MainWindow.AdministrativeEmployee.Name;

            LoadRoutes();
            LoadVehicles();
        }

        private void LoadRoutes()
        {
            RouteService routeService = new RouteService();

            RouteComboBox.ItemsSource = routeService.GetAllData();
        }

        private void LoadVehicles()
        {
            VehicleService vehicleService = new VehicleService();

            VehicleComboBox.ItemsSource = vehicleService.GetAllData();
        }
    }
}
