using Entity;
using System.Windows;
using System.Windows.Controls;

using static Entity.FinalcialInformationType;

namespace View
{
    /// <summary>
    /// Interaction logic for CurrentTransportFormUserControl.xaml
    /// </summary>
    public partial class CurrentTransportFormUserControl : UserControl, IAfterRegister
    {
        public static TransportForm CurrentTransportForm;
        public CurrentTransportFormUserControl()
        {
            InitializeComponent();
            SetTransportFormInfo();
        }

        private void SetTransportFormInfo()
        {
            if (CurrentTransportForm is null)
                return;

            TransportFormID.Text += CurrentTransportForm.Number;
            TransportFormDate.Text += CurrentTransportForm.Date.ToShortDateString();
            TransportFormDispatcher.Text += CurrentTransportForm.Dispatcher.Name;
            VehiclePlate.Text += CurrentTransportForm.Vehicle.LicensePlate;
            VehicleDriver.Text += CurrentTransportForm.Vehicle.Driver.Name;
            Route.Text += $"{CurrentTransportForm.Route.OriginCity} a {CurrentTransportForm.Route.DestinationCity} con costo de {CurrentTransportForm.Route.Cost}";
            PassengersView.Passengers.ItemsSource = CurrentTransportForm.Tickets;

            FinancialInformationFields.ReplacementFundField.Text = CurrentTransportForm.FinalcialInformation[REPLACEMENT_FUND].ToString();
            FinancialInformationFields.SocialContributionField.Text = CurrentTransportForm.FinalcialInformation[SOCIAL_CONTRIBUTION].ToString();
            FinancialInformationFields.TireServiceField.Text = CurrentTransportForm.FinalcialInformation[TIRE_SERVICE].ToString();
            FinancialInformationFields.VehicleFixServiceField.Text = CurrentTransportForm.FinalcialInformation[VEHICLE_FIX_SERVICE].ToString();
            FinancialInformationFields.NonConSecServiceField.Text = CurrentTransportForm.FinalcialInformation[NON_CONTRACTUAL_SERCURE_SERVICE].ToString();
            FinancialInformationFields.ConstInsuServiceField.Text = CurrentTransportForm.FinalcialInformation[CONSTACT_INSURANCE_SERVICE].ToString();
            FinancialInformationFields.SocialProtectionField.Text = CurrentTransportForm.FinalcialInformation[SOCIAL_PROTECTION].ToString();
            FinancialInformationFields.SocialContributionField.Text = CurrentTransportForm.FinalcialInformation[SOCIAL_CONTRIBUTION].ToString();
            FinancialInformationFields.ExtraordinaryProtectionField.Text = CurrentTransportForm.FinalcialInformation[EXTRAORDINARY_PROTECTION].ToString();
            FinancialInformationFields.AdministrationField.Text = CurrentTransportForm.FinalcialInformation[ADMINISTRATION].ToString();
            FinancialInformationFields.OthersField.Text = CurrentTransportForm.FinalcialInformation[OTHERS].ToString();
            FinancialInformationFields.SubTotal.Text = CurrentTransportForm.FinalcialInformation.Total.ToString();

            TotalTransportForm.Text = "Total planilla: " + CurrentTransportForm.TotalValue.ToString();
        }

        public void AfterRegister()
        {
            AddNewTicket.IsOpen = false;
            PassengersView.Passengers.ItemsSource = CurrentTransportForm.Tickets;
            PassengersView.Passengers.Items.Refresh();
            CurrentTransportForm.UpdateTotalValue();
            TotalTransportForm.Text = "Total planilla: " + CurrentTransportForm.TotalValue.ToString();
        }

        private void AddNewPassenger_Click(object sender, RoutedEventArgs e)
        {
            AddNewTicket.Child = new RegisterTicketUserControl(this, CloseAddNewTicket);
            AddNewTicket.IsOpen = true;
        }

        private void CloseAddNewTicket()
        {
            AddNewTicket.IsOpen = false;
        }

        private void SaveTransportForm_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
