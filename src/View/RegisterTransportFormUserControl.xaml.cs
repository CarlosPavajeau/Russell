using Common;
using Entity;
using System;
using System.Collections;
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
            if (await MainWindow.Client.Send(ClientRequest.GetRoutes))
            {
                RouteComboBox.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;

                if (await MainWindow.Client.Send(ClientRequest.GetVehicles))
                    VehicleComboBox.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;

                if (await MainWindow.Client.Send(ClientRequest.GetTransportFormCount))
                    TransportFormID.Text += ((int)await MainWindow.Client.ReceiveObject() + 1).ToString("00000");
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
                if (await MainWindow.Client.Send(ClientRequest.GetTransportFormCount))
                {
                    string transportFormNumber = ((int)await MainWindow.Client.ReceiveObject() + 1).ToString("00000");

                    CurrentTransportFormUserControl.CurrentTransportForm = new TransportForm(transportFormNumber, RouteComboBox.SelectedItem as Route, VehicleComboBox.SelectedItem as Vehicle, MainWindow.AdministrativeEmployee);

                    if (await MainWindow.Client.Send(TypeCommand.Save, TypeData.TransportForm, CurrentTransportFormUserControl.CurrentTransportForm))
                        HandleServerAnswer();
                }
            }
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SaveSuccessful)
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
