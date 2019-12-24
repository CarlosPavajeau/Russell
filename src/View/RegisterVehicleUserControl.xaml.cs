using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterVehicleUserControl.xaml
    /// </summary>
    public partial class RegisterVehicleUserControl : UserControl
    {
        Employee _driver, _owner;
        public RegisterVehicleUserControl()
        {
            InitializeComponent();
            _driver = _owner = null;
        }

        private async void SaveVehileButton_Click(object sender, RoutedEventArgs e)
        {
            if (_driver is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.EMPLOYEE, DriverField.Text))
                    _driver = await MainWindow.Client.ReceiveObject() as Employee;
            }

            if (_owner is null)
                if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.EMPLOYEE, OwnerField.Text))
                    _owner = await MainWindow.Client.ReceiveObject() as Employee;

            if (_driver is null || _owner is null)
                return;

            Vehicle vehicle = new Vehicle(PlateField.Text, InternalNumberField.Text, PropertyCardNumberField.Text, _owner, _driver);

            vehicle.Imprint.ChassisNumber = ChassisNumberField.Text;
            vehicle.Imprint.EngineNumber = EngineNumberField.Text;

            vehicle.Feature.Color = ColorField.Text;
            vehicle.Feature.Chairs = int.Parse(ChairsField.Text);
            vehicle.Feature.Mark = MarkField.Text;
            vehicle.Feature.Model = ModelField.Text;
            vehicle.Feature.ModelNumber = ModelNumberField.Text;
            vehicle.Feature.Type = TypeField.Text;

            vehicle.AddLegalInformation(LegalInformationType.SOAT, SoatDueDate.SelectedDate.Value, SoatDateOfRenovation.SelectedDate.Value);
            vehicle.AddLegalInformation(LegalInformationType.LICENSE, LicenseDueDate.SelectedDate.Value, LicenseDateOfRenovation.SelectedDate.Value);
            vehicle.AddLegalInformation(LegalInformationType.OPERATIONCARD, OperationCardDueDate.SelectedDate.Value, OperationCardDateOfRanovation.SelectedDate.Value);
            vehicle.AddLegalInformation(LegalInformationType.BIMONTHLYREVIEW, BiMonthlyDueDate.SelectedDate.Value, BiMonthlyDateOfRenovation.SelectedDate.Value);
            vehicle.AddLegalInformation(LegalInformationType.TECHNOMECHANICALREVIEW,
                                        TechnreviewDueDate.SelectedDate.Value, TechnreviewDateOfRenovation.SelectedDate.Value);

            if (await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.VEHICLE, vehicle))
                HandleServerAnswer();   
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
                MessageBox.Show("Datos guardados con exito");
            else
                MessageBox.Show("Vehiculo ya registrado");
        }

        private void SearchOwner_Click(object sender, RoutedEventArgs e)
        {
            SearchDriveOrOwner.Child = new EmployeesViewUserControl(SetOwner, CloseSearchDriverOrOwner);
            SearchDriveOrOwner.IsOpen = true;
        }

        private void SetOwner(Person person)
        {
            OwnerField.Text = person.ID;
            _owner = person as Employee;
            CloseSearchDriverOrOwner();
        }

        private void SearchDriver_Click(object sender, RoutedEventArgs e)
        {
            SearchDriveOrOwner.Child = new EmployeesViewUserControl(SetDriver, CloseSearchDriverOrOwner);
            SearchDriveOrOwner.IsOpen = true;
        }

        private void SetDriver(Person person)
        {
            DriverField.Text = person.ID;
            _driver = person as Employee;
            CloseSearchDriverOrOwner();
        }

        private void CloseSearchDriverOrOwner()
        {
            SearchDriveOrOwner.IsOpen = false;
        }
    }
}
