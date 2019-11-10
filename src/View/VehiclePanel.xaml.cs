using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for Vehicle.xaml
    /// </summary>
    public partial class VehiclePanel : UserControl
    {
        public VehiclePanel()
        {
            InitializeComponent();
            VehicleMainGrid.Children.Add(new VehicleMainPanel());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            VehicleMainGrid.Children.Clear();
            VehicleMainGrid.Children.Add(new RegisterVehicle());
        }
    }
}
