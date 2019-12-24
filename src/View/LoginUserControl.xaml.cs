using Common;
using Entity;
using System;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for LoginUserControl.xaml
    /// </summary>
    public partial class LoginUserControl : UserControl
    {
        public LoginUserControl(LoginAction action)
        {
            InitializeComponent();
            Action = action;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e) => Environment.Exit(0);

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.ADMINISTRATIVE_EMPLOYEE, UserField.Text))
                HandleRecieveObject();
        }

        private async void HandleRecieveObject()
        {
            object obj = await MainWindow.Client.ReceiveObject();

            if (obj is AdministrativeEmployee administrativeEmployee)
            {
                if (administrativeEmployee.User.AccessData.Password == PasswordField.Password)
                    Dispatcher.Invoke(() => Action?.Invoke(administrativeEmployee));
                else
                    MessageBox.Show("Contraseña incorrecta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (obj is ServerAnswer answer)
            {
                if (answer == ServerAnswer.NOT_FOUND_DATA)
                    MessageBox.Show("Usuario no registrado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public delegate void LoginAction(AdministrativeEmployee administrativeEmployee);

        private readonly LoginAction Action;
    }
}
