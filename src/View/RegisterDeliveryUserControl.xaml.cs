using Common;
using Entity;
using System;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterDeliveryUserControl.xaml
    /// </summary>
    public partial class RegisterDeliveryUserControl : UserControl
    {
        internal Person Sender, Receiver;

        public RegisterDeliveryUserControl()
        {
            InitializeComponent();
            LoadData();
            Sender = Receiver = null;
        }

        private async void LoadData()
        {
            if (await MainWindow.Client.Send(ClientRequest.GetDeliveriesCount))
            {
                DeliveryNumber.Text += ((int)await MainWindow.Client.ReceiveObject() + 1).ToString("00000");

                if (await MainWindow.Client.Send(ClientRequest.GetDestinations))
                {
                    DestinationComboBox.ItemsSource = await MainWindow.Client.ReceiveObject() as IEnumerable;
                    DeliveryDate.Text += DateTime.Now.ToShortDateString();
                    DeliveryDispatcher.Text += $"{MainWindow.AdministrativeEmployee.Name}";
                }
            }
        }

        public void ShowRegisterPerson(string personId, UserControl owner)
        {
            RegisterPerson.Child = new RegisterPersonUserControl(CloseRegisterPerson, personId);
            RegisterPerson.IsOpen = true;
            RegisterPerson.PlacementTarget = owner;
            RegisterPerson.Placement = PlacementMode.Center;
        }

        private void SearchSenderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchReceiverOrReceiver.Child = new PeopleViewUserControl(SetSender, ClosePeopleView);
            SearchReceiverOrReceiver.IsOpen = true;
        }

        private void SetSender(Person person)
        {
            ClosePeopleView();
            Sender = person;
            SenderField.Text = person.ID;
        }

        private void SearchReceiverButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SearchReceiverOrReceiver.Child = new PeopleViewUserControl(SetReceiver, ClosePeopleView);
            SearchReceiverOrReceiver.IsOpen = true;
        }

        private void SetReceiver(Person person)
        {
            ClosePeopleView();
            Receiver = person;
            ReceiverField.Text = person.ID;
        }

        private void ClosePeopleView()
        {
            SearchReceiverOrReceiver.IsOpen = false;
        }

        private void CloseRegisterPerson()
        {
            RegisterPerson.IsOpen = false;
        }
    }
}
