using BusinessLogicLayer;
using Entity;
using System.Windows;
using System.Windows.Controls;
using Common;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterCommendUserControl.xaml
    /// </summary>
    public partial class RegisterCommendUserControl : UserControl, IReception<Vehicle>
    {
        Vehicle Vehicle;
        public RegisterCommendUserControl()
        {
            InitializeComponent();
            Vehicle = null;
        }

        public void Receive(Vehicle data)
        {
            CloseSearchVehicle();
            Vehicle = data;
            VehiclePlateField.Text = data.LicensePlate;
        }

        private async void RegisterCommendButton_Click(object sender, RoutedEventArgs e)
        {
            if (DeliveryFields.Sender is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Person, DeliveryFields.SenderField))
                    DeliveryFields.Sender = await MainWindow.Client.ReceiveObject() as Person;
            }

            if (DeliveryFields.Receiver is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Person, DeliveryFields.ReceiverField))
                    DeliveryFields.Receiver = await MainWindow.Client.ReceiveObject() as Person;
            }

            if (Vehicle is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Vehicle, VehiclePlateField.Text))
                    Vehicle = await MainWindow.Client.ReceiveObject() as Vehicle;
            }

            if (DeliveryFields.Sender is null)
            {
                MessageBoxResult result = MessageBox.Show("Remitente no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.SenderField.Text, this);
                }

                return;
            }
            if (DeliveryFields.Receiver is null)
            {
                MessageBoxResult result = MessageBox.Show("Destinario no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.ReceiverField.Text, this);
                }

                return;
            }
            if (Vehicle is null)
            {
                MessageBox.Show("Vehículo no registrado.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(FreightValueField.Text, out int freightValue))
            {
                MessageBox.Show("Valor del flete invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           
            if (!int.TryParse(AgreementField.Text, out int agreement))
            {
                MessageBox.Show("Convenio de encomienda invalido", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (await MainWindow.Client.Send(ClientRequest.GetDeliveriesCount))
            {
                string deliveryNumber = ((int)await MainWindow.Client.ReceiveObject() + 1).ToString("00000");

                Commend commend = new Commend(deliveryNumber, DeliveryFields.Sender, DeliveryFields.Receiver, MainWindow.AdministrativeEmployee,
                                              DeliveryFields.DestinationComboBox.SelectedItem as string, CommendDescriptionField.Text,
                                              freightValue, agreement, Vehicle);

                if (await MainWindow.Client.Send(TypeCommand.Save, TypeData.Commend, commend))
                    HandleServerAnswer();
            }
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SaveSuccessful)
                MessageBox.Show("Datos guardados");
            else
                MessageBox.Show("Error al guardar los datos");

        }

        private void SearchVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            SearchVehicle.Child = new LittleVehiclesViewUserControl(this, CloseSearchVehicle);
            SearchVehicle.IsOpen = true;
        }

        private void CloseSearchVehicle()
        {
            SearchVehicle.IsOpen = false;
        }
    }
}
