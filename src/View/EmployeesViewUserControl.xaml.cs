using Common;
using Entity;
using System.Collections;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for EmployeesViewUserControl.xaml
    /// </summary>
    public partial class EmployeesViewUserControl : UserControl
    {
        readonly CloseAction _closeAction;
        readonly SelectPerson _selectPerson;

        public EmployeesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        public EmployeesViewUserControl(SelectPerson selectPerson, CloseAction closeAction) : this()
        {
            _closeAction = closeAction;
            _selectPerson = selectPerson;
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GET_ALL_EMPLOYEES))
                Employees.ItemsSource = await MainWindow.Client.RecieveObject() as IEnumerable;
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _selectPerson?.Invoke(Employees.SelectedItem as Employee);
        }
    }
}
