using System.Windows;
using System.Windows.Controls;

using Entity;
using Entity.Common;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterBankDraftUserControl.xaml
    /// </summary>
    public partial class RegisterBankDraftUserControl : UserControl
    {
        public RegisterBankDraftUserControl()
        {
            InitializeComponent();
        }

        private void RegisterBankDraftButton_Click(object sender, RoutedEventArgs e)
        {
            Person psender, receiver;

            psender = receiver = null;

            int.TryParse(ValueToSendField.Text, out int valueToSend);
            int.TryParse(CostField.Text, out int cost);

            BankDraft bankDraft = new BankDraft(psender, receiver, MainWindow.AdministrativeEmployee, DeliveryFields.DestinationField.Text, valueToSend, cost);

            MainWindow.Client.ReceiveData = ReceiveData;

            MainWindow.Client.Send(new SearchCommand(TypeData.PERSON), DeliveryFields.SenderField.Text);
        }

        private void ReceiveData(object data)
        {

        }
    }
}
