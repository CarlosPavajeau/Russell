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
    /// Interaction logic for SettingsUserControl.xaml
    /// </summary>
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();
        }

        private void UserSettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AccessDataSettingsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SetMainPanel(UserControl userControl)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(userControl);
        }
    }
}
