using BusinessLogicLayer;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterCommendUserControl.xaml
    /// </summary>
    public partial class RegisterCommendUserControl : UserControl, IReception<Vehicle>
    {
        public RegisterCommendUserControl()
        {
            InitializeComponent();
        }

        public void Receive(Vehicle data)
        {
            CloseSearchVehicle();
            VehiclePlateField.Text = data.LicensePlate;
        }

        private void RegisterCommendButton_Click(object sender, RoutedEventArgs e)
        {
            Person psender, receiver;
            Vehicle vehicle;
            PersonService personService = new PersonService();
            VehicleService vehicleService = new VehicleService();

            psender = personService.Search(DeliveryFields.SenderField.Text);
            receiver = personService.Search(DeliveryFields.ReceiverField.Text);
            vehicle = vehicleService.Search(VehiclePlateField.Text);

            if (psender is null)
            {
                MessageBoxResult result = MessageBox.Show("Remitente no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.SenderField.Text, this);
                }

                return;
            }
            if (receiver is null)
            {
                MessageBoxResult result = MessageBox.Show("Destinario no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.ReceiverField.Text, this);
                }

                return;
            }
            if (vehicle is null)
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
            
            CommendService commendService = new CommendService();

            string deliveryNumber = commendService.Count.ToString();

            Commend commend = new Commend(deliveryNumber, psender, receiver, MainWindow.AdministrativeEmployee, 
                                          DeliveryFields.DestinationComboBox.SelectedItem as string, CommendDescriptionField.Text,
                                          freightValue, agreement, vehicle);
            

            if (commendService.Save(commend))
            {
                MessageBox.Show("Datos guardados");
                TotalCommend.Text += commend.Total;
                DeliveryFields.DeliveryNumber.Text += commend.Number;
            }
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
