using System.Threading;
using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer.Client;
using Entity;
using Entity.Common;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static readonly Client Client = new Client();
        public static AdministrativeEmployee AdministrativeEmployee;

        public MainWindow()
        {
            InitializeComponent();
            StartApp();
        }

        private void StartApp()
        {
            Client.ServerAnswer = IsFirstApplicationStart;
            Client.Connect();
            Client.Send(ClientRequest.IS_IT_THE_FIRST_APPLICATION_START);
        }

        private void IsFirstApplicationStart(ServerAnswer answer)
        {
            if (answer == ServerAnswer.IS_THE_FIRST_APPLICATION_START)
                Dispatcher.Invoke(new ThreadStart(() => SetMainPanel(new RegisterAdministrativeEmployeeUserControl())));
            else
                Dispatcher.Invoke(new ThreadStart(() => ShowLoginPanel()));
        }

        public void SetMainPanel(UserControl userControl)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(userControl);
        }

        private void LoginSucces()
        {

            WindowState = WindowState.Maximized;
            SetMainPanel(new MainPanel(LogOutAction));
        }

        private void LogOutAction()
        {
            ShowLoginPanel();
        }

        private void ShowLoginPanel()
        {
            WindowState = WindowState.Normal;
            Width = 400;
            Height = 550;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            SetMainPanel(new LoginUserControl(LoginSucces));
        }
    }
}
