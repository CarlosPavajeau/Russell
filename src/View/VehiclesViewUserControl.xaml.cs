using Common;
using System.Collections;
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

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GetVehicles))
                Vehicles.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
        }
    }
}
