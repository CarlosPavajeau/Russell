using Entity;
using System;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterDeliveryUserControl.xaml
    /// </summary>
    public partial class RegisterDeliveryUserControl : UserControl
    {
        public RegisterDeliveryUserControl()
        {
            InitializeComponent();
            DeliveryDate.Text += DateTime.Now.ToShortDateString();
            DeliveryDispatcher.Text += $"{MainWindow.AdministrativeEmployee.Name}";
        }

        private void SearchSenderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchReceiver.Child = new PeopleViewUserControl(SetSender);
            SearchReceiver.IsOpen = true;
        }

        private void SetSender(Person person)
        {
            SearchReceiver.IsOpen = false;
            SenderField.Text = person.ID;
        }

        private void SearchReceiverButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchReceiver.Child = new PeopleViewUserControl(SetReceiver);
            SearchReceiver.IsOpen = true;
        }

        private void SetReceiver(Person person)
        {
            SearchReceiver.IsOpen = false;
            ReceiverField.Text = person.ID;
        }
    }
}
