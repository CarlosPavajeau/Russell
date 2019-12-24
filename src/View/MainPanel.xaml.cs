using System;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : UserControl
    {
        public MainPanel(LogOutAction logOut)
        {
            InitializeComponent();
            LogOut = logOut;
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LogOut?.Invoke();
        }

        public delegate void LogOutAction();

        private readonly LogOutAction LogOut;

        private void BankDraftButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new BankDraftUserControl());
        }

        private void CommendButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new CommendUserControl());
        }

        private void TransportFormButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new TransportFormUserControl());
        }

        private void RoutesButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new RoutesUserControl());
        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new SettingsUserControl());
        }

        private void VehicleButton_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new VehicleUserControl());
        }

        private void SetMainPanel(UserControl userControl)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(userControl);
        }
    }
}
