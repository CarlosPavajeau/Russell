using System;
using System.Collections.Generic;

namespace Entity
{
    public class TransportForm
    {
        private static int transportFormsCount = 0;

        public TransportForm(string number, string originCity, string destinationCity, Vehicle vehicle,
                             Person dispatcher, List<Ticket> tickets, FinalcialInformation finalcialInformation)
        {
            Number = number;
            OriginCity = originCity;
            DestinationCity = destinationCity;
            Vehicle = vehicle;
            Dispatcher = dispatcher;
            Tickets = tickets;
            FinalcialInformation = finalcialInformation;
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

        public FinalcialInformation FinalcialInformation { get; }

        public Person Dispatcher { get; set; }
        public Vehicle Vehicle { get; set; }

        public void AddTicket(Person client, decimal price)
        {
            Ticket ticket = new Ticket($"{Number}-{(Tickets.Count + 1).ToString("00000")}", client, Vehicle, DestinationCity, price);
            AddTicket(ticket);
        }

        private void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            ValueOfTickets += ticket.Price;
        }

        public void UpdateTotalValue()
        {
            TotalValue = ValueOfTickets + FinalcialInformation.CalculateTotal();
        }
    }
}
