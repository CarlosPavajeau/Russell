﻿using BusinessLogicLayer;
using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

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

        private async void RegisterEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string id, firstName, secondName, lastName, secondLastName, cellphone, email, address, user, password;
            TypeUser typeUser;

            id = RegisterEmployeeFields.RegisterPersonFields.IDField.Text;
            firstName = RegisterEmployeeFields.RegisterPersonFields.FirstNameField.Text;
            secondName = RegisterEmployeeFields.RegisterPersonFields.SecondNameField.Text;
            lastName = RegisterEmployeeFields.RegisterPersonFields.LastNameField.Text;
            secondLastName = RegisterEmployeeFields.RegisterPersonFields.SecondLastNameField.Text;
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

            await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.ADMINISTRATIVE_EMPLOYEE, administrativeEmployee);
            HandleServerAnswer();
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
            {
                Dispatcher.Invoke(() => MessageBox.Show("Empleado registrado con exito"));
                Dispatcher.Invoke(() => _afterRegister.AfterRegister());
            }
            else if (answer == ServerAnswer.DATA_ALREADY_REGISTERED)
            {
                Dispatcher.Invoke(() => MessageBox.Show("Usuario ya registrado"));
            }
        }
    }
}
