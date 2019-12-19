using BusinessLogicLayer;
using Entity;
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

        private void LoadData()
        {
            EmployeeService employeeService = new EmployeeService();
            Employees.ItemsSource = employeeService.GetAllData();
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
