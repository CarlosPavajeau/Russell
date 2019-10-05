using System;
using System.Collections.Generic;

namespace Entity
{
    public class TransportForm
    {
        private string originCity;
        private string destinationCity;

        public TransportForm()
        {
            Tickets = new List<Ticket>();
        }

        public string Number { get; }

        public string OriginCity
        {
            get
            {
                return originCity;
            }
            set
            {
                originCity = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The origin city is invalid");
            }
        }

        public string DestinationCity
        {
            get
            {
                return destinationCity;
            }
            set
            {
                destinationCity = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The number is invalid");
            }
        }

        public decimal ValueOfTickets { get; private set; }

        public decimal TotalValue { get; private set; }
        public DateTime Date { get; set; }

        public DateTime DepartureTime { get; set; }
        public List<Ticket> Tickets { get; }

        public FinalcialInformation FinalcialInformation { get; set; }

        public Person Dispatcher { get; set; }

        public void AddTicket(Ticket ticket)
        {
            Tickets.Add(ticket);
            ValueOfTickets += ticket.Price;
            UpdateTotalValue(ticket.Price);
        }

        public void UpdateTotalValue(decimal amount)
        {
            TotalValue += amount;
        }
    }
}
