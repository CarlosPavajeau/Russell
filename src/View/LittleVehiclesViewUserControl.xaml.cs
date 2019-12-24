using Entity;
using System.Collections;
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
        readonly CloseAction _closeAction;
        public LittleVehiclesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(Common.ClientRequest.GET_ALL_VEHICLES))
                Vehicles.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
        }

        public LittleVehiclesViewUserControl(IReception<Vehicle> reception, CloseAction closeAction) : this()
        {
            _reception = reception;
            _closeAction = closeAction;
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _reception?.Receive(Vehicles.SelectedItem as Vehicle);
        }

        private void ReturnButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }
    }
}
