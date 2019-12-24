using Common;
using Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

            LoadData();

            AfterRegister = afterRegister;
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GET_ALL_ROUTES_AND_VEHICLES))
            {
                List<IEnumerable> routesAndVehicles = await MainWindow.Client.RecieveObject() as List<IEnumerable>;

                RouteComboBox.ItemsSource = routesAndVehicles[0];
                VehicleComboBox.ItemsSource = routesAndVehicles[1];
            }
        }

        private async void GenerateTransportFormButton_Click(object sender, RoutedEventArgs e)
        {
            if (!(CurrentTransportFormUserControl.CurrentTransportForm is null))
            {
                MessageBoxResult result = MessageBox.Show("Ya tiene registrada una planilla. ¿Desea verla?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                    AfterRegister?.Invoke();
            }
            else
            {
                CurrentTransportFormUserControl.CurrentTransportForm = new TransportForm("000", RouteComboBox.SelectedItem as Route, VehicleComboBox.SelectedItem as Vehicle, MainWindow.AdministrativeEmployee);

                if (await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.TRANSPORT_FORM, CurrentTransportFormUserControl.CurrentTransportForm))
                    HandleServerAnswer();
            }
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
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
