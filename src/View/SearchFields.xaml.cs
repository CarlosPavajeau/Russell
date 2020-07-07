using System.Windows;
using System.Windows.Controls;

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

        public SearchFields(SearchAction search)
        {
            Search = search;
        }

        public delegate void SearchAction(object sender, RoutedEventArgs e);

        private SearchAction Search;

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Search?.Invoke(sender, e);
        }
    }
}
