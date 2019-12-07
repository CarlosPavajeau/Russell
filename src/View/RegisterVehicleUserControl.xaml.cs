using BusinessLogicLayer;
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
        public RegisterVehicleUserControl()
        {
            InitializeComponent();
        }

        private void SaveVehileButton_Click(object sender, RoutedEventArgs e)
        {
            Employee driver, owner;
            EmployeeService employeeService = new EmployeeService();

            driver = employeeService.Search(DriverField.Text);
            owner = employeeService.Search(OwnerField.Text);

            if (driver is null || owner is null)
                return;

            Vehicle vehicle = new Vehicle(PlateField.Text, InternalNumberField.Text, PropertyCardNumberField.Text, owner, driver);

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

            VehicleService vehicleService = new VehicleService();

            if (vehicleService.Save(vehicle))
                MessageBox.Show("Datos guardados con exito");
            else
                MessageBox.Show("Vehiculo ya registrado");
        }
    }
}
