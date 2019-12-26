using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View
{
    /// <summary>
    /// Interaction logic for DispatcherMainPanel.xaml
    /// </summary>
    public partial class DispatcherMainPanel : UserControl
    {
        readonly LogOutAction LogOut;
        public DispatcherMainPanel(LogOutAction logOut)
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

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new SettingsUserControl());
        }

        private void SetMainPanel(UserControl userControl)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(userControl);
        }
    }
}
