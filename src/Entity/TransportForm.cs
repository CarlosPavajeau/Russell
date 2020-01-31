using System;
using System.Collections.Generic;

using static Entity.FinalcialInformationType;

namespace Entity
{
    [System.Serializable]
    public class TransportForm
    {
        public TransportForm(string number, Route route, Vehicle vehicle, AdministrativeEmployee dispatcher) : this(number, route, vehicle, dispatcher, DateTime.Now, DateTime.Now)
        {

        }

        public TransportForm(string number, Route route, Vehicle vehicle, AdministrativeEmployee dispatcher, DateTime startDate,
                             DateTime depatureTime, bool state = true)
        {
            Number = number;
            State = state;
            Route = route;
            Vehicle = vehicle;
            Dispatcher = dispatcher;
            Date = startDate;
            DepartureTime = depatureTime;
            Tickets = new List<Ticket>();
            FinalcialInformation = new FinalcialInformation();
            SetFinalcialInformation();
        }

        private void SetFinalcialInformation()
        {
            FinalcialInformation[ReplacementFund] = 0;
            FinalcialInformation[SocialContribution] = 0;
            FinalcialInformation[TireService] = 0;
            FinalcialInformation[VehicleFixService] = 0;
            FinalcialInformation[NonContractualSecureService] = 0;
            FinalcialInformation[ConstactInsuranceService] = 0;
            FinalcialInformation[SocialProtection] = 0;
            FinalcialInformation[ExtraordinaryProtection] = 0;
            FinalcialInformation[Administration] = 0;
            FinalcialInformation[Others] = 0;
        }

        public string Number { get; }

        public bool State { get; set; }

        public Route Route { get; set; }
        public decimal ValueOfTickets { get; private set; }

        public decimal TotalValue { get; private set; }

        public DateTime Date { get; set; }
        public DateTime DepartureTime { get; set; }
        public List<Ticket> Tickets { get; }

        public FinalcialInformation FinalcialInformation { get; }

        public AdministrativeEmployee Dispatcher { get; set; }
        public Vehicle Vehicle { get; set; }

        public void AddFinalcialInformation(FinalcialInformationType type, decimal value)
        {
            FinalcialInformation[type] = value;
        }

        public void AddTicket(Passenger passenger, int seats)
        {
            AddTicket(passenger, seats, DateTime.Now);
        }

        public void AddTicket(Passenger passenger, int seats, DateTime date)
        {
            Ticket ticket = new Ticket($"{Number}-{(Tickets.Count + 1).ToString("000")}", passenger, Vehicle, Route, seats, date);
            AddTicket(ticket);
        }

        private void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            ValueOfTickets += ticket.Total;
        }

        public void RemoveTicket(Ticket ticket)
        {
            Tickets.Remove(ticket);
            ValueOfTickets -= ticket.Total;
        }

        public void UpdateTotalValue()
        {
            TotalValue = ValueOfTickets + FinalcialInformation.Total;
        }
    }
}
