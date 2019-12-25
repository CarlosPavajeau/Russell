using BusinessLogicLayer.Client;
using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IAfterRegister
    {
        const int LOGIN_PANEL_WIDTH = 400;
        const int LOGIN_PANEL_HEIGHT = 550;

        public static AdministrativeEmployee AdministrativeEmployee;
        public static Client Client = new Client();

        public MainWindow()
        {
            InitializeComponent();
            StartApp();
        }

        private async void StartApp()
        {
            if (await Client.Connect())
            {
                if (await Client.Send(ClientRequest.IS_IT_THE_FIRST_APPLICATION_START))
                    IsFirstApplicationStart();
            }
        }

        private async void IsFirstApplicationStart()
        {
            ServerAnswer answer = await Client.RecieveServerAnswer();

            if (answer == ServerAnswer.IS_THE_FIRST_APPLICATION_START)
                Dispatcher.Invoke(() => SetMainPanel(new RegisterAdministrativeEmployeeUserControl(this)));
            else
                Dispatcher.Invoke(() => ShowLoginPanel());
        }

        private void LoginSucces(AdministrativeEmployee administrativeEmployee)
        {
            WindowState = WindowState.Maximized;
            AdministrativeEmployee = administrativeEmployee;
            SetMainPanel(new MainPanel(LogOutAction));
            LoadCurrentTransportForm();
        }

        private async void LoadCurrentTransportForm()
        {
            if (await Client.Send(TypeCommand.SEARCH, TypeData.CURRENT_TRANSPORT_FORM, AdministrativeEmployee.ID))
                CurrentTransportFormUserControl.CurrentTransportForm = await Client.ReceiveObject() as TransportForm;
        }

        private void LogOutAction()
        {
            ShowLoginPanel();
        }

        private void ShowLoginPanel()
        {
            WindowState = WindowState.Normal;
            Width = LOGIN_PANEL_WIDTH;
            Height = LOGIN_PANEL_HEIGHT;
            SetMainPanel(new LoginUserControl(LoginSucces));
        }

        private void SetMainPanel(UserControl userControl)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(userControl);
        }

        public void AfterRegister()
        {
            ShowLoginPanel();
        }
    }
}
