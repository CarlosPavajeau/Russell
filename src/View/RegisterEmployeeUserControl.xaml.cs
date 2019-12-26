using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterEmployeeUserControl.xaml
    /// </summary>
    public partial class RegisterEmployeeUserControl : UserControl
    {
        public RegisterEmployeeUserControl()
        {
            InitializeComponent();
        }

        private async void RegisterEmployee_Click(object sender, RoutedEventArgs e)
        {
            string id, firstName, secondName, lastName, secondLastName, cellphone, email, address;

            id = RegisterEmployeesField.RegisterPersonFields.IDField.Text;
            firstName = RegisterEmployeesField.RegisterPersonFields.FirstNameField.Text;
            secondName = RegisterEmployeesField.RegisterPersonFields.SecondNameField.Text;
            lastName = RegisterEmployeesField.RegisterPersonFields.LastNameField.Text;
            secondLastName = RegisterEmployeesField.RegisterPersonFields.SecondLastNameField.Text;
            cellphone = RegisterEmployeesField.CellphoneField.Text;
            email = RegisterEmployeesField.EmailField.Text;
            address = RegisterEmployeesField.AddressField.Text;

            Employee employee = new Employee(id, firstName, secondName, lastName, secondLastName, cellphone, email, address);

            if (await MainWindow.Client.Send(TypeCommand.Save, TypeData.Employee, employee))
                HandleServerAnswer();               
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SaveSuccessful)
                MessageBox.Show("Registro exitoso");
            else
                MessageBox.Show("Datos ya registrados");
        }
    }
}
