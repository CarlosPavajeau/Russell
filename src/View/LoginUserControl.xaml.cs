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

        private void ExitButton_Click(object sender, RoutedEventArgs e) => MainWindow.Exit();

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (await MainWindow.Client.Send(TypeCommand.Search, TypeData.AdministrativeEmployee, UserField.Text))
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
                if (answer == ServerAnswer.NotFoundData)
                    MessageBox.Show("Usuario no registrado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public delegate void LoginAction(AdministrativeEmployee administrativeEmployee);

        private readonly LoginAction Action;

        private void PasswordField_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                LoginButton_Click(null, null);
        }
    }
}
