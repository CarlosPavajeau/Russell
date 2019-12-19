using BusinessLogicLayer;
using Entity;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for PeopleViewUserControl.xaml
    /// </summary>
    public partial class PeopleViewUserControl : UserControl
    {

        private readonly SelectPerson _selectPerson;
        private readonly CloseAction _closeAction;
        public PeopleViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        public PeopleViewUserControl(SelectPerson selectPerson, CloseAction closeAction) : this()
        {
            _selectPerson = selectPerson;
            _closeAction = closeAction;
        }

        private void LoadData()
        {
            PersonService personService = new PersonService();
            People.ItemsSource = personService.GetAllData();
        }

        private void DataGridRow_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _selectPerson?.Invoke(People.SelectedItem as Person);
        }

        private void CloseButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _closeAction?.Invoke();
        }
    }
}
