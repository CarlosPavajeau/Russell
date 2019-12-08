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
    /// Interaction logic for VehicleUserControl.xaml
    /// </summary>
    public partial class VehicleUserControl : UserControl
    {
        public VehicleUserControl()
        {
            InitializeComponent();
        }

        private void RegisterVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new RegisterVehicleUserControl());
        }

        private void SearchVehicleButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new SearchVehicleUserControl());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new VehiclesViewUserControl());
        }
    }
}
