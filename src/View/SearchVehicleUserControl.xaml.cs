using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for SearchVehicleUserControl.xaml
    /// </summary>
    public partial class SearchVehicleUserControl : UserControl
    {
        public SearchVehicleUserControl()
        {
            InitializeComponent();
        }

        private void SearchFields_Loaded(object sender, RoutedEventArgs e)
        {
            SearchFields.SearchTitleField.Text = "Placa: ";
            SearchFields.SearchButton.Click += SearchButton_Click;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.Vehicle, SearchFields.SearchField.Text))
            {
                if ((await MainWindow.Client.ReceiveObject()) is Vehicle vehicle)
                {
                    MainPanel.Children.Clear();
                    MainPanel.Children.Add(new VehicleViewUserControl(vehicle));
                }
                else
                {

                }
            }
        }
    }
}
