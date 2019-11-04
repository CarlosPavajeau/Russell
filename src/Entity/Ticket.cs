using System;

namespace Entity
{
    public class Ticket
    {
        private int seats;

        public Ticket(string number, Passenger passenger, Vehicle vehicle, Route route, int seats)
        {
            Number = number;
            Route = route;
            Seats = seats;
            Passenger = passenger;
            Vehicle = vehicle;
            Date = DateTime.Now;
            Total = Route.Cost * Seats;
        }

        public Ticket(string number, Passenger passenger, Vehicle vehicle, Route route, int seats, DateTime date)
        {
            Number = number;
            Route = route;
            Seats = seats;
            Passenger = passenger;
            Vehicle = vehicle;
            Date = date;
            Total = Route.Cost * Seats;
        }

        public string Number { get; }

        public Route Route { get; set; }

        public int Seats
        {
            get => seats;
            set => seats = (value >= 0) ? value : throw new ArgumentException("The seats are invalid");
        }

        public decimal Total { get; }

        public Passenger Passenger { get; set; }

        public Vehicle Vehicle { get; set; }

        public DateTime Date { get; }
    }
}
