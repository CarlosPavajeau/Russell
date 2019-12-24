using Common;
using Entity;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterPersonUserControl.xaml
    /// </summary>
    public partial class RegisterPersonUserControl : UserControl
    {
        readonly CloseAction _closeAction;

        public RegisterPersonUserControl()
        {
            InitializeComponent();
        }

        public RegisterPersonUserControl(CloseAction closeAction, string personId) : this()
        {
            _closeAction = closeAction;
            PersonFields.IDField.Text = personId;
            PersonFields.IDField.IsEnabled = false;
        }

        private async void RegisterPerson_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person(PersonFields.IDField.Text, PersonFields.FirstNameField.Text, PersonFields.SecondNameField.Text,
                                       PersonFields.LastNameField.Text, PersonFields.SecondNameField.Text);

            if (await MainWindow.Client.Send(TypeCommand.SAVE, TypeData.PERSON, person))
                HandleServerAnswer();
        }

        private async void HandleServerAnswer()
        {
            ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

            if (answer == ServerAnswer.SAVE_SUCCESSFUL)
            {
                MessageBox.Show("Registro exitoso");
                Close();
            }
            else
                MessageBox.Show("Datos ya registrados");
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close()
        {
            _closeAction?.Invoke();
        }
    }
}
