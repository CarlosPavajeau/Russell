using Entity;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for PassegersViewUserControl.xaml
    /// </summary>
    public partial class PassegersViewUserControl : UserControl
    {
        public PassegersViewUserControl()
        {
            InitializeComponent();
        }

        public void AddPassenger(Ticket ticket)
        {
            Passengers.Items.Add(ticket);
        }
    }
}
