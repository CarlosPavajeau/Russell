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
        readonly Recieve<Employee> _selectEmployee;

        public EmployeesViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        public EmployeesViewUserControl(Recieve<Employee> selectEmployee, CloseAction closeAction) : this()
        {
            _closeAction = closeAction;
            _selectEmployee = selectEmployee;
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GetEmployees))
                Employees.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _selectEmployee?.Invoke(Employees.SelectedItem as Employee);
        }
    }
}
