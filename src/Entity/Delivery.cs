using System;

namespace Entity
{
    public enum State
    {
        ACTIVE,
        DELIVERED,
        CANCEL
    }
    public abstract class Delivery
    {
        public Delivery(string number, DateTime date, Person sender, Person receiver, AdministrativeEmployee dispatcher, string destination, State state)
        {
            Number = number;
            Date = date;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            Destination = destination;
            State = state;
        }

        public Delivery(string number, Person sender, Person receiver, AdministrativeEmployee dispatcher, string destination, State state = State.ACTIVE) :
                        this(number, DateTime.Now, sender, receiver, dispatcher, destination, state)
        {

        }

        public string Number { get; }

        public DateTime Date { get; }

        public Person Sender { get; set; }

        public Person Receiver { get; set; }

        public AdministrativeEmployee Dispatcher { get; set; }

        public State State { get; set; }

        public string Destination { get; set; }

        public decimal Total { get; protected set; }

        public bool IsActived()
        {
            return State == State.ACTIVE;
        }

        public bool IsDelivered()
        {
            return State == State.DELIVERED;
        }
    }
}
