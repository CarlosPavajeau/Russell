using System;

namespace Entity
{
    public class Ticket
    {
        private int seats;

        public Ticket(string number, Person client, Vehicle vehicle, Route route, int seats)
        {
            Number = number;
            Route = route;
            Amount = seats;
            Client = client;
            Vehicle = vehicle;
            Date = DateTime.Now;
            Total = Route.Cost * Amount;
        }

        public Ticket(string number, Person client, Vehicle vehicle, Route route, int seats, DateTime date)
        {
            Number = number;
            Route = route;
            Amount = seats;
            Client = client;
            Vehicle = vehicle;
            Date = date;
            Total = Route.Cost * Amount;
        }

        public string Number { get; }

        public Route Route { get; set; }

        public int Amount
        {
            get => seats;
            set => seats = (value >= 0) ? value : throw new ArgumentException("The price is invalid");
        }

        public decimal Total { get; }

        public Person Client { get; set; }

        public Vehicle Vehicle { get; set; }

        public DateTime Date { get; }
    }
}
