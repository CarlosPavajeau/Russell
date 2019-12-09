using BusinessLogicLayer;
using Entity;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterTicketUserControl.xaml
    /// </summary>
    public partial class RegisterTicketUserControl : UserControl
    {
        private Passenger _passenger;
        readonly IAfterRegister _afterRegister;
        readonly CloseAction _closeAction;
        public RegisterTicketUserControl()
        {
            InitializeComponent();
            TicketDate.Text += DateTime.Now.ToShortDateString();
            TicketDispatcher.Text += MainWindow.AdministrativeEmployee.Name;
        }

        public RegisterTicketUserControl(IAfterRegister afterRegister, CloseAction closeAction) : this()
        {
            _afterRegister = afterRegister;
            _closeAction = closeAction;
        }

        private void SearhPassenger_Click(object sender, RoutedEventArgs e)
        {
            SelectPerson.Child = new PeopleViewUserControl(SetPassenger);
            SelectPerson.IsOpen = true;
        }

        private void SetPassenger(Person person)
        {
            SelectPerson.IsOpen = false;
            _passenger = Person.ToPassenger(person);
            PassenderID.Text = _passenger.ID;
        }

        private void AddNewPassenger_Click(object sender, RoutedEventArgs e)
        {
            if (_passenger is null)
            {
                PersonService personService = new PersonService();
                _passenger = Person.ToPassenger(personService.Search(PassenderID.Text));

                if (_passenger is null)
                    return;
            }

            CurrentTransportFormUserControl.CurrentTransportForm?.AddTicket(_passenger, int.Parse(SeatsField.Text));

            TransportFormService transportFormService = new TransportFormService();
            if (transportFormService.Save(CurrentTransportFormUserControl.CurrentTransportForm.Tickets.Last()))
                _afterRegister?.AfterRegister();
            else
                MessageBox.Show("Error");
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }
    }
}
