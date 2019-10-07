using System;
using System.Collections.Generic;

namespace Entity
{
    public class TransportForm
    {
        private static int transportFormsCount = 0;

        public TransportForm(string number, string originCity, string destinationCity, Vehicle vehicle, Person dispatcher)
        {
            Number = number;
            OriginCity = originCity;
            DestinationCity = destinationCity;
            Vehicle = vehicle;
            Dispatcher = dispatcher;
            Tickets = new List<Ticket>();
            FinalcialInformation = new FinalcialInformation();
        }

        public TransportForm(string originCity, string destinationCity, Vehicle vehicle, Person dispatcher)
        {
            Number = (++transportFormsCount).ToString("00000");
            OriginCity = originCity;
            DestinationCity = destinationCity;
            Vehicle = vehicle;
            Dispatcher = dispatcher;
            Date = DateTime.Now;
            Tickets = new List<Ticket>();
            FinalcialInformation = new FinalcialInformation();
        }
        public TransportForm()
        {
            Number = (++transportFormsCount).ToString("00000");
            Date = DateTime.Now;
            Tickets = new List<Ticket>();
            FinalcialInformation = new FinalcialInformation();
        }

        public string Number { get; }

        public string OriginCity { get; set; }
        public string DestinationCity { get; set; }
        public decimal ValueOfTickets { get; private set; }

        public decimal TotalValue { get; private set; }

        public DateTime Date { get; set; }
        public DateTime DepartureTime { get; set; }
        public List<Ticket> Tickets { get; }

        public FinalcialInformation FinalcialInformation { get; set; }

        public Person Dispatcher { get; set; }
        public Vehicle Vehicle { get; set; }

        private void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            ValueOfTickets += ticket.Price;
            UpdateTotalValue();
        }

        public void AddTicket(Person client, decimal price)
        {
            Ticket ticket = new Ticket($"{Number}-{(Tickets.Count + 1).ToString("00000")}", client, Vehicle, DestinationCity, price);
            AddTicket(ticket);
        }

        public void UpdateTotalValue()
        {
            TotalValue = CalculateFinalcialInformation() + ValueOfTickets;
        }

        private decimal CalculateFinalcialInformation()
        {
            decimal accumulatedValue = 0;

            accumulatedValue += FinalcialInformation.Administration;
            accumulatedValue += FinalcialInformation.ConstactInsuranceService;
            accumulatedValue += FinalcialInformation.ExtraordinaryProtection;
            accumulatedValue += FinalcialInformation.NonContractualSecureService;
            accumulatedValue += FinalcialInformation.Others;
            accumulatedValue += FinalcialInformation.ReplacementFund;
            accumulatedValue += FinalcialInformation.SocialContribution;
            accumulatedValue += FinalcialInformation.SocialProtection;
            accumulatedValue += FinalcialInformation.TireService;
            accumulatedValue += FinalcialInformation.VehicleFixService;

            return accumulatedValue;
        }
    }
}
