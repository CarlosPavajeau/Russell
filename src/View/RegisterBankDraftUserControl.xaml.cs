using System.Windows;
using System.Windows.Controls;
using BusinessLogicLayer;
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
            PersonService personService = new PersonService();

            psender = personService.Search(DeliveryFields.SenderField.Text);
            receiver = personService.Search(DeliveryFields.ReceiverField.Text);

            if (psender is null)
                return;
            if (receiver is null)
                return;

            int.TryParse(ValueToSendField.Text, out int valueToSend);
            int.TryParse(CostField.Text, out int cost);

            BankDraft bankDraft = new BankDraft(psender, receiver, MainWindow.AdministrativeEmployee, DeliveryFields.DestinationField.Text, valueToSend, cost);

            BankDraftService bankDraftService = new BankDraftService();

            if (bankDraftService.Save(bankDraft))
                MessageBox.Show("Datos guardadados");
            else
                MessageBox.Show("Error al guardar los datos");
        }
    }
}
