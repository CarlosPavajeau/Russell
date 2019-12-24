using Common;
using Entity;
using System.Collections;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for PeopleViewUserControl.xaml
    /// </summary>
    public partial class PeopleViewUserControl : UserControl
    {

        private readonly SelectPerson _selectPerson;
        private readonly CloseAction _closeAction;
        public PeopleViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        public PeopleViewUserControl(SelectPerson selectPerson, CloseAction closeAction) : this()
        {
            _selectPerson = selectPerson;
            _closeAction = closeAction;
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GET_ALL_PEOPLE))
                People.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _selectPerson?.Invoke(People.SelectedItem as Person);
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }
    }
}
