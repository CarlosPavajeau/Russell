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
    /// Interaction logic for UserSettingsUserControl.xaml
    /// </summary>
    public partial class UserSettingsUserControl : UserControl
    {
        public UserSettingsUserControl()
        {
            InitializeComponent();
        }

        private void AddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new RegisterEmployeeUserControl());
        }

        private void AddNewAdministrativeEmployee_Click(object sender, RoutedEventArgs e)
        {
            SetMainPanel(new RegisterAdministrativeEmployeeUserControl());
        }

        private void SetMainPanel(UserControl userControl)
        {
            MainPanel.Children.Clear();
            MainPanel.Children.Add(userControl);
        }

    }
}
