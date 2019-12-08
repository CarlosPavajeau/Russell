using BusinessLogicLayer;
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

        private void LoadData()
        {
            BankDraftService bankDraftService = new BankDraftService();
            BankDrafts.ItemsSource = bankDraftService.GetAllData();
        }
    }
}
