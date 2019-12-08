using Entity;
using System;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterTicketUserControl.xaml
    /// </summary>
    public partial class RegisterTicketUserControl : UserControl
    {
        public RegisterTicketUserControl()
        {
            InitializeComponent();
            TicketDate.Text += DateTime.Now.ToShortDateString();
            TicketDispatcher.Text += MainWindow.AdministrativeEmployee.Name;
        }

        private void SearhPassenger_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectPerson.Child = new PeopleViewUserControl(SetPassenger);
            SelectPerson.IsOpen = true;
        }

        private void SetPassenger(Person person)
        {
            SelectPerson.IsOpen = false;
            PassenderID.Text = person.ID;
        }
    }
}
