using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Entity;
using Entity.Common;

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
            MainWindow.Client.ReceiveData = SetAdministrativeEmployee;
            MainWindow.Client.ServerAnswer = HandleServerAnswer;

            DataPacket dataPacket = new DataPacket(Command.SEARCH_ADMINISTRATIVE_EMPLOYEE, UserField.Text);

            MainWindow.Client.Send(dataPacket);
        }

        private void SetAdministrativeEmployee(object data)
        {
            if (data is AdministrativeEmployee administrativeEmployee)
            {
                MainWindow.AdministrativeEmployee = administrativeEmployee;
                Dispatcher.Invoke(new ThreadStart(() => Action?.Invoke()));
            }
        }

        private void HandleServerAnswer(ServerAnswer answer)
        {
            if (answer == ServerAnswer.NOT_FOUND_DATA)
                MessageBox.Show("Usuario no registrado", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public delegate void LoginAction();

        private readonly LoginAction Action;
    }
}
