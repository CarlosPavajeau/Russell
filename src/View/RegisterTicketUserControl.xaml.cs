using Common;
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
            SelectPerson.Child = new PeopleViewUserControl(SetPassenger, CloseSelectPerson);
            SelectPerson.IsOpen = true;
        }

        private void CloseSelectPerson()
        {
            SelectPerson.IsOpen = false;
        }

        private void SetPassenger(Person person)
        {
            SelectPerson.IsOpen = false;
            _passenger = person.ToPassenger();
            PassenderID.Text = _passenger.ID;
        }

        private async void AddNewPassenger_Click(object sender, RoutedEventArgs e)
        {
            if (_passenger is null)
            {
                if (await MainWindow.Client.Send(TypeCommand.SEARCH, TypeData.PERSON, PassenderID.Text))
                    _passenger = (await MainWindow.Client.ReceiveObject() as Person).ToPassenger();

                if (_passenger is null)
                    return;
            }

            CurrentTransportFormUserControl.CurrentTransportForm?.AddTicket(_passenger, int.Parse(SeatsField.Text));

            if (await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.TICKET, CurrentTransportFormUserControl.CurrentTransportForm.Tickets.Last()))
                HandleServerAnswer();
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
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
