using System.Windows.Controls;
using BusinessLogicLayer;

namespace View
{
    /// <summary>
    /// Interaction logic for CommendsViewUserControl.xaml
    /// </summary>
    public partial class CommendsViewUserControl : UserControl
    {
        public CommendsViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            CommendService commendService = new CommendService();
            Commends.ItemsSource = commendService.GetAllData();
        }
    }
}
