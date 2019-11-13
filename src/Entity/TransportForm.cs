using System;
using System.Collections.Generic;

namespace Entity
{
    public class TransportForm
    {
        private static int transportFormsCount = 0;

        public TransportForm(string number, Route route, Vehicle vehicle,
                             AdministrativeEmployee dispatcher, List<Ticket> tickets, FinalcialInformation finalcialInformation)
        {
            Number = number;
            Route = route;
            Vehicle = vehicle;
            Dispatcher = dispatcher;
            Tickets = tickets;
            FinalcialInformation = finalcialInformation;
        }

        public TransportForm(Route route, Vehicle vehicle, AdministrativeEmployee dispatcher)
        {
            Number = (++transportFormsCount).ToString("00000");
            Route = route;
            Vehicle = vehicle;
            Dispatcher = dispatcher;
            Date = DateTime.Now;
            Tickets = new List<Ticket>();
            FinalcialInformation = new FinalcialInformation();
        }

        public string Number { get; }

        public Route Route { get; set; }
        public decimal ValueOfTickets { get; private set; }

        public decimal TotalValue { get; private set; }

        public DateTime Date { get; set; }
        public DateTime DepartureTime { get; set; }
        public List<Ticket> Tickets { get; }

        public FinalcialInformation FinalcialInformation { get; }

        public AdministrativeEmployee Dispatcher { get; set; }
        public Vehicle Vehicle { get; set; }

        public void AddTicket(Passenger passenger, int seats)
        {
            Ticket ticket = new Ticket($"{Number}-{(Tickets.Count + 1).ToString("00000")}", passenger, Vehicle, Route, seats);
            AddTicket(ticket);
        }

        private void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            ValueOfTickets += ticket.Total;
        }

        public void UpdateTotalValue()
        {
            TotalValue = ValueOfTickets + FinalcialInformation.CalculateTotal();
        }
    }
}
