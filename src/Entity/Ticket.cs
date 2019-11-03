using System;

namespace Entity
{
    public class Ticket
    {
        private decimal price;

        public Ticket(string number, Person client, Vehicle vehicle, string destination, decimal price)
        {
            Number = number;
            Destination = destination;
            Price = price;
            Client = client;
            Vehicle = vehicle;
            Date = DateTime.Now;
        }

        public Ticket(string number, Person client, Vehicle vehicle, string destination, decimal price, DateTime date)
        {
            Number = number;
            Destination = destination;
            Price = price;
            Client = client;
            Vehicle = vehicle;
            Date = date;
        }

        public string Number { get; }

        public string Destination { get; set; }

        public decimal Price
        {
            get => price;
            set => price = (value >= 0) ? value : throw new ArgumentException("The price is invalid");
        }

        public Person Client { get; set; }

        public Vehicle Vehicle { get; set; }

        public DateTime Date { get; }
    }
}
