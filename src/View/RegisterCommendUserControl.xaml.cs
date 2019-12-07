using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Entity;
using BusinessLogicLayer;

namespace View
{
    /// <summary>
    /// Interaction logic for RegisterCommendUserControl.xaml
    /// </summary>
    public partial class RegisterCommendUserControl : UserControl
    {
        public RegisterCommendUserControl()
        {
            InitializeComponent();
        }

        private void RegisterCommendButton_Click(object sender, RoutedEventArgs e)
        {
            Person psender, receiver;
            Vehicle vehicle;
            PersonService personService = new PersonService();
            VehicleService vehicleService = new VehicleService();

            psender = personService.Search(DeliveryFields.SenderField.Text);
            receiver = personService.Search(DeliveryFields.ReceiverField.Text);
            vehicle = vehicleService.Search(VehiclePlateField.Text);

            if (psender is null)
                return;
            if (receiver is null)
                return;
            if (vehicle is null)
                return;

            int.TryParse(FreightValueField.Text, out int freightValue);
            int.TryParse(AgreementField.Text, out int agreement);

            Commend commend = new Commend(psender, receiver, MainWindow.AdministrativeEmployee, DeliveryFields.DestinationField.Text, CommendDescriptionField.Text,
                                          freightValue, agreement, vehicle);
            CommendService commendService = new CommendService();

            if (commendService.Save(commend))
                MessageBox.Show("Datos guardados");
            else
                MessageBox.Show("Error al guardar los datos");
        }
    }
}
