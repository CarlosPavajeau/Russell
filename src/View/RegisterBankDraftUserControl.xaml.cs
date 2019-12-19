using BusinessLogicLayer;
using Entity;
using System.Windows;
using System.Windows.Controls;

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
            {
                MessageBoxResult result = MessageBox.Show("Remitente no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.SenderField.Text, this);
                }

                return;
            }
            if (receiver is null)
            {
                MessageBoxResult result = MessageBox.Show("Destinario no registrado. ¿Desea registrarlo?", "Advertencia", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    DeliveryFields.ShowRegisterPerson(DeliveryFields.ReceiverField.Text, this);
                }

                return;
            }

            if (!int.TryParse(ValueToSendField.Text, out int valueToSend))
            {
                MessageBox.Show("Valor a enviar invalido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(CostField.Text, out int cost))
            {
                MessageBox.Show("Costo invalido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            

            BankDraftService bankDraftService = new BankDraftService();

            string deliveryNumber = bankDraftService.Count.ToString();

            BankDraft bankDraft = new BankDraft(deliveryNumber, psender, receiver, MainWindow.AdministrativeEmployee, 
                                                DeliveryFields.DestinationComboBox.SelectedItem as string, valueToSend, cost);

            

            if (bankDraftService.Save(bankDraft))
            {
                MessageBox.Show("Datos guardadados");
                TotalBankDraft.Text += bankDraft.Total;
                DeliveryFields.DeliveryNumber.Text += bankDraft.Number;
                
            }
            else
                MessageBox.Show("Error al guardar los datos");
        }
    }
}
