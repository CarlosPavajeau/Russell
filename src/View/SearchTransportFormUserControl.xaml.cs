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
    /// Interaction logic for SearchTransportFormUserControl.xaml
    /// </summary>
    public partial class SearchTransportFormUserControl : UserControl
    {
        public SearchTransportFormUserControl()
        {
            InitializeComponent();
        }

        private void SearchFields_Loaded(object sender, RoutedEventArgs e)
        {
            SearchFields.SearchTitleField.Text = "Código planilla: ";
        }
    }
}
