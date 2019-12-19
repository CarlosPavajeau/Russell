using BusinessLogicLayer;
using Entity;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
            LoadData();
        }

        private void LoadData()
        {
            DeliveryDate.Text += DateTime.Now.ToShortDateString();
            DeliveryDispatcher.Text += $"{MainWindow.AdministrativeEmployee.Name}";

            RouteService routeService = new RouteService();
            DestinationComboBox.ItemsSource = routeService.Destinations;
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
