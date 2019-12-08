using BusinessLogicLayer;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RoutesViewUserControl.xaml
    /// </summary>
    public partial class RoutesViewUserControl : UserControl
    {
        public RoutesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            RouteService routeService = new RouteService();
            Routes.ItemsSource = routeService.GetAllData();
        }
    }
}
