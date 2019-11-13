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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
