using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Entity;
using Entity.Common;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterAdministrativeEmployeeUserControl.xaml
    /// </summary>
    public partial class RegisterAdministrativeEmployeeUserControl : UserControl
    {
        private IAfterRegister _afterRegister;
        public RegisterAdministrativeEmployeeUserControl()
        {
            InitializeComponent();
        }

        public RegisterAdministrativeEmployeeUserControl(IAfterRegister afterRegister)
        {
            InitializeComponent();
            _afterRegister = afterRegister;
            TypeOfUserComboBox.Visibility = Visibility.Hidden;
            TypeUserText.Text += " Superusuario.";
        }

        private void RegisterEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string id, firstName, secondName, lastName, secondLastName, cellphone, email, address, user, password;
            TypeUser typeUser;

            id = RegisterEmployeeFields.RegisterPersonFields.IDField.Text;
            firstName = RegisterEmployeeFields.RegisterPersonFields.FirstNameField.Text;
            secondName = RegisterEmployeeFields.RegisterPersonFields.SecondNameField.Text;
            lastName = RegisterEmployeeFields.RegisterPersonFields.LastNameField.Text;
            secondLastName = RegisterEmployeeFields.RegisterPersonFields.SecondNameField.Text;
            cellphone = RegisterEmployeeFields.CellphoneField.Text;
            email = RegisterEmployeeFields.EmailField.Text;
            address = RegisterEmployeeFields.AddressField.Text;

            user = UserField.Text;
            password = PasswordField.Text;

            if (!(_afterRegister is null))
                typeUser = TypeUser.SUPERUSER;
            else
                typeUser = (TypeOfUserComboBox.SelectedIndex == 0) ? TypeUser.SUPERUSER :
                           (TypeOfUserComboBox.SelectedIndex == 1) ? TypeUser.ADMIN : TypeUser.DISPATCHER;

            AdministrativeEmployee administrativeEmployee = new AdministrativeEmployee(id, firstName, secondName, lastName,
                                                                                       secondLastName, cellphone, email, address, new User(user, password, typeUser));

            MainWindow.Client.ServerAnswer = ProcessServerAnswer;
            MainWindow.Client.Send(new SaveCommand(TypeData.ADMINISTRATIVE_EMPLOYEE), administrativeEmployee);
        }

        private void ProcessServerAnswer(ServerAnswer answer)
        {
            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
                Dispatcher.Invoke(new ThreadStart(() => _afterRegister?.AfterRegister()));
        }
    }
}
