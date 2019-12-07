using BusinessLogicLayer;
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
        public RegisterPersonUserControl()
        {
            InitializeComponent();
        }

        private void RegisterPerson_Click(object sender, RoutedEventArgs e)
        {
            Person person = new Person(PersonFields.IDField.Text, PersonFields.FirstNameField.Text, PersonFields.SecondNameField.Text,
                                       PersonFields.LastNameField.Text, PersonFields.SecondNameField.Text);
            PersonService personService = new PersonService();

            if (personService.Save(person))
                MessageBox.Show("Registro exitoso");
            else
                MessageBox.Show("Datos ya registrados");
        }
    }
}
