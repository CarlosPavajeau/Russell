using System;

namespace Entity
{
    public class Ticket
    {
        private string destination;
        private decimal price;

        public Ticket(string number, string destination, decimal price, Person client, Vehicle vehicle)
        {
            Number = number;
            Destination = destination;
            Price = price;
            Client = client;
            Vehicle = vehicle;

        }

        public string Number { get; }

        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The destination is invalid");
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = (value > 0) ? value : throw new ArgumentException("The price is invalid");
            }
        }

        public Person Client { get; set; }

        public Vehicle Vehicle { get; set; }
    }
}
