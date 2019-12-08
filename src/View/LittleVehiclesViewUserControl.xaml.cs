using BusinessLogicLayer;
using Entity;
using System.Windows.Controls;
using System.Windows.Input;

namespace View
{
    /// <summary>
    /// Interaction logic for LittleVehiclesViewUserControl.xaml
    /// </summary>
    public partial class LittleVehiclesViewUserControl : UserControl
    {
        readonly IReception<Vehicle> _reception;
        public LittleVehiclesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            VehicleService vehicleService = new VehicleService();
            Vehicles.ItemsSource = vehicleService.GetAllData();
        }

        public LittleVehiclesViewUserControl(IReception<Vehicle> reception) : this()
        {
            _reception = reception;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _reception?.Receive(Vehicles.SelectedItem as Vehicle);
        }
    }
}
