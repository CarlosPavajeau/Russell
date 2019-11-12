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

        private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Action?.Invoke();
        }

        public delegate void LoginAction();

        private readonly LoginAction Action;
    }
}
