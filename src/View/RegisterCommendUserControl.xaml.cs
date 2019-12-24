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
                if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.PERSON, DeliveryFields.SenderField))
                    DeliveryFields.Sender = await MainWindow.Client.RecieveObject() as Person;
            }

            if (DeliveryFields.Receiver is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.PERSON, DeliveryFields.ReceiverField))
                    DeliveryFields.Receiver = await MainWindow.Client.RecieveObject() as Person;
            }

            if (Vehicle is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.VEHICLE, VehiclePlateField.Text))
                    Vehicle = await MainWindow.Client.RecieveObject() as Vehicle;
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

            string deliveryNumber = "0000";

            Commend commend = new Commend(deliveryNumber, DeliveryFields.Sender, DeliveryFields.Receiver, MainWindow.AdministrativeEmployee, 
                                          DeliveryFields.DestinationComboBox.SelectedItem as string, CommendDescriptionField.Text,
                                          freightValue, agreement, Vehicle);

            if (await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.COMMEND, commend))
                HandleServerAnswer();  
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
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
