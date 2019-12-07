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
                MessageBox.Show("Usuario no registrado");
            else
                Action?.Invoke(administrativeEmployee);
        }

        public delegate void LoginAction(AdministrativeEmployee administrativeEmployee);

        private readonly LoginAction Action;
    }
}
