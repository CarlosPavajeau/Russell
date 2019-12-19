using BusinessLogicLayer;
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();

            AdministrativeEmployee administrativeEmployee = administrativeEmployeeService.Search(UserField.Text);

            if (administrativeEmployee is null)
                MessageBox.Show("Usuario no registrado", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            else
            {
                if (administrativeEmployee.User.AccessData.Password == PasswordField.Password)
                    Action?.Invoke(administrativeEmployee);
                else
                    MessageBox.Show("Contraseña incorrecta", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public delegate void LoginAction(AdministrativeEmployee administrativeEmployee);

        private readonly LoginAction Action;
    }
}
