using BusinessLogicLayer.Client;
using Common;
using Entity;
using System;
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
                if (await Client.Send(ClientRequest.IsTheFirstApplicationStart))
                    IsFirstApplicationStart();
            }
        }

        private async void IsFirstApplicationStart()
        {
            ServerAnswer answer = await Client.RecieveServerAnswer();

            if (answer == ServerAnswer.IsTheFirstApplicationStart)
                Dispatcher.Invoke(() => SetMainPanel(new RegisterAdministrativeEmployeeUserControl(this)));
            else
                Dispatcher.Invoke(() => ShowLoginPanel());
        }

        private void LoginSucces(AdministrativeEmployee administrativeEmployee)
        {
            WindowState = WindowState.Maximized;
            AdministrativeEmployee = administrativeEmployee;

            if (AdministrativeEmployee.User.IsDispatcher())
                SetMainPanel(new DispatcherMainPanel(LogOutAction));
            else
                SetMainPanel(new MainPanel(LogOutAction));

            LoadCurrentTransportForm();
        }

        private async void LoadCurrentTransportForm()
        {
            if (await Client.Send(TypeCommand.Search, TypeData.CurrentTransportForm, AdministrativeEmployee.ID))
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

        public static void Exit()
        {
            MessageBoxResult result = MessageBox.Show("¿Esta seguro?", "Informacion", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
                Environment.Exit(0);
        }
    }
}
