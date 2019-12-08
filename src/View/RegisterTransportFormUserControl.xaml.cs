using BusinessLogicLayer;
using Entity;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterTransportFormUserControl.xaml
    /// </summary>
    public partial class RegisterTransportFormUserControl : UserControl
    {
        readonly AfterRegister _afterRegister;
        public RegisterTransportFormUserControl(AfterRegister afterRegister)
        {
            InitializeComponent();

            TransportFormDate.Text += DateTime.Now.ToShortDateString();
            TransportFormDispatcher.Text += MainWindow.AdministrativeEmployee.Name;

            LoadRoutes();
            LoadVehicles();

            _afterRegister = afterRegister;
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

        private void GenerateTransportFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CurrentTransportFormUserControl.CurrentTransportForm is null))
            {
                MessageBox.Show("Ya tiene registrada una planilla");
                _afterRegister?.Invoke();
            }
            else
            {
                CurrentTransportFormUserControl.CurrentTransportForm = new TransportForm(RouteComboBox.SelectedItem as Route, VehicleComboBox.SelectedItem as Vehicle, MainWindow.AdministrativeEmployee);

                TransportFormService transportFormService = new TransportFormService();

                if (transportFormService.Save(CurrentTransportFormUserControl.CurrentTransportForm))
                {
                    MessageBox.Show("Planilla registrada");
                    _afterRegister?.Invoke();
                }
                else
                {
                    MessageBox.Show("Error");
                    CurrentTransportFormUserControl.CurrentTransportForm = null;
                }
            }
        }
    }
}
