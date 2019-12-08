using BusinessLogicLayer;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for VehiclesViewUserControl.xaml
    /// </summary>
    public partial class VehiclesViewUserControl : UserControl
    {
        public VehiclesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            VehicleService vehicleService = new VehicleService();
            Vehicles.ItemsSource = vehicleService.GetAllData();
        }
    }
}
