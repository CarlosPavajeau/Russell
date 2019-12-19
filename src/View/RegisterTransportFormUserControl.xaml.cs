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
        event AfterRegister AfterRegister;
        public RegisterTransportFormUserControl(AfterRegister afterRegister)
        {
            InitializeComponent();

            TransportFormDate.Text += DateTime.Now.ToShortDateString();
            TransportFormDispatcher.Text += MainWindow.AdministrativeEmployee.Name;

            LoadRoutes();
            LoadVehicles();

            AfterRegister = afterRegister;
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
                MessageBoxResult result = MessageBox.Show("Ya tiene registrada una planilla. ¿Desea verla?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                    AfterRegister?.Invoke();
            }
            else
            {
                TransportFormService transportFormService = new TransportFormService();
                CurrentTransportFormUserControl.CurrentTransportForm = new TransportForm((transportFormService.Count + 1).ToString(),RouteComboBox.SelectedItem as Route, VehicleComboBox.SelectedItem as Vehicle, MainWindow.AdministrativeEmployee);

                

                if (transportFormService.Save(CurrentTransportFormUserControl.CurrentTransportForm))
                {
                    MessageBox.Show("Planilla registrada");
                    AfterRegister?.Invoke();
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
