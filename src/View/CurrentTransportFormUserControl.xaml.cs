using Entity;
using Common;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

using static Entity.FinalcialInformationType;
using System.Threading.Tasks;
using System.Collections.Generic;

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

            FinancialInformationFields.ReplacementFundField.Text = CurrentTransportForm.FinalcialInformation[ReplacementFund].ToString();
            FinancialInformationFields.SocialContributionField.Text = CurrentTransportForm.FinalcialInformation[SocialContribution].ToString();
            FinancialInformationFields.TireServiceField.Text = CurrentTransportForm.FinalcialInformation[TireService].ToString();
            FinancialInformationFields.VehicleFixServiceField.Text = CurrentTransportForm.FinalcialInformation[VehicleFixService].ToString();
            FinancialInformationFields.NonConSecServiceField.Text = CurrentTransportForm.FinalcialInformation[NonContractualSecureService].ToString();
            FinancialInformationFields.ConstInsuServiceField.Text = CurrentTransportForm.FinalcialInformation[ConstactInsuranceService].ToString();
            FinancialInformationFields.SocialProtectionField.Text = CurrentTransportForm.FinalcialInformation[SocialProtection].ToString();
            FinancialInformationFields.SocialContributionField.Text = CurrentTransportForm.FinalcialInformation[SocialContribution].ToString();
            FinancialInformationFields.ExtraordinaryProtectionField.Text = CurrentTransportForm.FinalcialInformation[ExtraordinaryProtection].ToString();
            FinancialInformationFields.AdministrationField.Text = CurrentTransportForm.FinalcialInformation[Administration].ToString();
            FinancialInformationFields.OthersField.Text = CurrentTransportForm.FinalcialInformation[Others].ToString();
            FinancialInformationFields.SubTotal.Text = CurrentTransportForm.FinalcialInformation.Total.ToString();

            TotalTransportForm.Text = "Total planilla: " + CurrentTransportForm.TotalValue.ToString();
        }

        public void AfterRegister()
        {
            CloseAddNewTicket();

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

        private async void SaveTransportForm_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<string, Dictionary<FinalcialInformationType, decimal>> valuesToUpdate = new KeyValuePair<string, Dictionary<FinalcialInformationType, decimal>>(CurrentTransportForm.Number, new Dictionary<FinalcialInformationType, decimal>());

            AddFinancialInformationIfChange(ReplacementFund, valuesToUpdate.Value, FinancialInformationFields.ReplacementFundField.Text);
            AddFinancialInformationIfChange(SocialContribution, valuesToUpdate.Value, FinancialInformationFields.SocialContributionField.Text);
            AddFinancialInformationIfChange(TireService, valuesToUpdate.Value, FinancialInformationFields.TireServiceField.Text);
            AddFinancialInformationIfChange(VehicleFixService, valuesToUpdate.Value, FinancialInformationFields.VehicleFixServiceField.Text);
            AddFinancialInformationIfChange(NonContractualSecureService, valuesToUpdate.Value, FinancialInformationFields.NonConSecServiceField.Text);
            AddFinancialInformationIfChange(ConstactInsuranceService, valuesToUpdate.Value, FinancialInformationFields.ConstInsuServiceField.Text);
            AddFinancialInformationIfChange(SocialProtection, valuesToUpdate.Value, FinancialInformationFields.SocialProtectionField.Text);
            AddFinancialInformationIfChange(ExtraordinaryProtection, valuesToUpdate.Value, FinancialInformationFields.ExtraordinaryProtectionField.Text);
            AddFinancialInformationIfChange(Administration, valuesToUpdate.Value, FinancialInformationFields.AdministrationField.Text);
            AddFinancialInformationIfChange(Others, valuesToUpdate.Value, FinancialInformationFields.OthersField.Text);

            if (valuesToUpdate.Value.Count == 0)
            {
                SaveResult.Text = "La información financiera se encuentra actualizada.";
                SaveResult.Foreground = Brushes.Blue;
            }
            else
            {
                if (await MainWindow.Client.Send(TypeCommand.Update, TypeData.TransportForm, valuesToUpdate))
                {
                    if (await MainWindow.Client.RecieveServerAnswer() == ServerAnswer.SuccessfullyModified)
                    {
                        SaveResult.Text = "Información financiera actualizada éxitosamente!";
                        SaveResult.Foreground = Brushes.ForestGreen;
                        UpdateCurrentTransportForm(valuesToUpdate.Value);
                    }
                    else
                    {
                        SaveResult.Text = "Información financiera no actualizada, por favor revise los valores digitados.";
                        SaveResult.Foreground = Brushes.IndianRed;
                    }
                }
            }

            SaveResult.Visibility = Visibility.Visible;
            await Task.Run(() => Thread.Sleep(3000));
            SaveResult.Visibility = Visibility.Hidden;
        }

        private void UpdateCurrentTransportForm(Dictionary<FinalcialInformationType, decimal> newValues)
        {
            foreach (var value in newValues) 
                CurrentTransportForm.FinalcialInformation[value.Key] = value.Value;
            CurrentTransportForm.UpdateTotalValue();
            FinancialInformationFields.SubTotal.Text = CurrentTransportForm.FinalcialInformation.Total.ToString();
            TotalTransportForm.Text = "Total planilla: " + CurrentTransportForm.TotalValue.ToString();
        }

        private void AddFinancialInformationIfChange(FinalcialInformationType type, Dictionary<FinalcialInformationType, decimal> valuesToUpdate, string newValueStr)
        {
            if (decimal.TryParse(newValueStr, out decimal newValue))
            {
                if (newValue != CurrentTransportForm.FinalcialInformation[type])
                    valuesToUpdate[type] = newValue;
            }
        }

        private async void DeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            if (PassengersView.Passengers.SelectedItem is null)
            {
                MessageBox.Show("No ha seleccionado ningún pasajero.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            string ticket_number = (PassengersView.Passengers.SelectedItem as Ticket).Number;

            if (await MainWindow.Client.Send(TypeCommand.Delete, TypeData.Ticket, ticket_number))
            {
                ServerAnswer answer = await MainWindow.Client.RecieveServerAnswer();

                if (answer == ServerAnswer.SuccessfullyRemoved)
                {
                    MessageBox.Show("Pasajero eliminado con éxito.", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                    CurrentTransportForm.RemoveTicket(ticket_number);
                    CurrentTransportForm.UpdateTotalValue();
                    TotalTransportForm.Text = "Total planilla: " + CurrentTransportForm.TotalValue.ToString();
                    PassengersView.Passengers.Items.Refresh();
                }
                else
                    MessageBox.Show("Pasajero no eliminado, verifique si lo elimino anteriormente.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
