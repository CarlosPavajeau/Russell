using Common;
using System.Collections;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for BankDraftsViewUserControl.xaml
    /// </summary>
    public partial class BankDraftsViewUserControl : UserControl
    {
        public BankDraftsViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GetBankDrafts))
            {
                BankDrafts.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
            }
        }
    }
}
