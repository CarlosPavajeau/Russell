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
        public delegate void SelectPerson(Person person);

        private readonly SelectPerson _selectPerson;
        public PeopleViewUserControl()
        {
            InitializeComponent();
            LoadData();
        }

        public PeopleViewUserControl(SelectPerson selectPerson)
        {
            InitializeComponent();
            LoadData();
            _selectPerson = selectPerson;
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
    }
}
