using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for CommendUserControl.xaml
    /// </summary>
    public partial class CommendUserControl : UserControl
    {
        public CommendUserControl()
        {
            InitializeComponent();
        }

        private void RegisterCommendButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new RegisterCommendUserControl());
        }

        private void SearchCommendButton_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new SearchCommendUserControl());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new CommendsViewUserControl());
        }
    }
}
