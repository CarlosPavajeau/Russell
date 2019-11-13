using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for BankDraftUserControl.xaml
    /// </summary>
    public partial class BankDraftUserControl : UserControl
    {
        public BankDraftUserControl()
        {
            InitializeComponent();
        }

        private void RegisterBankDraftButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new RegisterBankDraftUserControl());
        }

        private void SearchBankDraftButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(new SearchBankDraftUserControl());
        }
    }
}
