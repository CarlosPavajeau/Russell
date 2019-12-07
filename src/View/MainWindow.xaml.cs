using BusinessLogicLayer;
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
        public static AdministrativeEmployee AdministrativeEmployee;

        public MainWindow()
        {
            InitializeComponent();
            StartApp();
        }

        private void StartApp()
        {
            AdministrativeEmployeeService administrativeEmployeeService = new AdministrativeEmployeeService();

            if (administrativeEmployeeService.IsEmpty())
                SetMainPanel(new RegisterAdministrativeEmployeeUserControl(this));
            else
                ShowLoginPanel();
        }

        public void SetMainPanel(UserControl userControl)
        {
            GridMain.Children.Clear();
            GridMain.Children.Add(userControl);
        }

        private void LoginSucces(AdministrativeEmployee administrativeEmployee)
        {
            WindowState = WindowState.Maximized;
            AdministrativeEmployee = administrativeEmployee;
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
            SetMainPanel(new LoginUserControl(LoginSucces));
        }

        public void AfterRegister()
        {
            ShowLoginPanel();
        }
    }
}
