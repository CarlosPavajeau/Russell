using Entity;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for VehicleViewUserControl.xaml
    /// </summary>
    public partial class VehicleViewUserControl : UserControl
    {
        public VehicleViewUserControl()
        {
            InitializeComponent();
        }

        public VehicleViewUserControl(Vehicle vehicle) : this()
        {
            LicensePlate.Text += vehicle.LicensePlate;
            InternalNumber.Text += vehicle.InternalNumber;
            PropertyCardNumber.Text += vehicle.PropertyCardNumber;

            ChassisNumber.Text += vehicle.Imprint.ChassisNumber;
            EngineNumber.Text += vehicle.Imprint.EngineNumber;

            Color.Text += vehicle.Feature.Color;
            Mark.Text += vehicle.Feature.Mark;
            Type.Text += vehicle.Feature.Type;
            Model.Text += vehicle.Feature.Model;
            ModelNumber.Text += vehicle.Feature.ModelNumber;
            Chairs.Text += vehicle.Feature.Chairs.ToString();

            OwnerId.Text += vehicle.Owner.ID;
            OwnerName.Text += vehicle.Owner.Name;
            DriverId.Text += vehicle.Driver.ID;
            DriverName.Text += vehicle.Driver.Name;

            SOAT.Text += $"{vehicle.LegalInformation[LegalInformationType.Soat].DateOfRenovation.ToShortDateString()} - {vehicle.LegalInformation[LegalInformationType.Soat].DueDate.ToShortDateString()}";
            License.Text += $"{vehicle.LegalInformation[LegalInformationType.License].DateOfRenovation.ToShortDateString()} - {vehicle.LegalInformation[LegalInformationType.License].DueDate.ToShortDateString()}";
            OperationCard.Text += $"{vehicle.LegalInformation[LegalInformationType.OperationCard].DateOfRenovation.ToShortDateString()} - {vehicle.LegalInformation[LegalInformationType.OperationCard].DueDate.ToShortDateString()}";
            BiMonthlyRevision.Text += $"{vehicle.LegalInformation[LegalInformationType.BiMonthlyReview].DateOfRenovation.ToShortDateString()} - {vehicle.LegalInformation[LegalInformationType.BiMonthlyReview].DueDate.ToShortDateString()}";
            Technreview.Text += $"{vehicle.LegalInformation[LegalInformationType.TechnomechanicalReview].DateOfRenovation.ToShortDateString()} - {vehicle.LegalInformation[LegalInformationType.TechnomechanicalReview].DueDate.ToShortDateString()}";
        }
    }
}
