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
    /// Interaction logic for SearchFields.xaml
    /// </summary>
    public partial class SearchFields : UserControl
    {
        public SearchFields()
        {
            InitializeComponent();
        }

        public delegate void SearchAction(object sender, RoutedEventArgs e);

        private SearchAction Search;

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Search?.Invoke(sender, e);
        }
    }
}
