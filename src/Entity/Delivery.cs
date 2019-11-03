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
        private static int deliveryCount = 0;

        public Delivery(string number, DateTime date, Person sender, Person receiver, Person dispatcher, string destination, State state)
        {
            Number = number;
            Date = date;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            Destination = destination;
            State = state;

        }

        public Delivery(Person sender, Person receiver, Person dispatcher, string destination, State state = State.ACTIVE)
        {
            Number = (++deliveryCount).ToString("00000");
            Date = DateTime.Now;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            Destination = destination;
            State = state;
        }

        public string Number { get; }

        public DateTime Date { get; }

        public Person Sender { get; set; }

        public Person Receiver { get; set; }

        public Person Dispatcher { get; set; }

        public State State { get; set; }

        public string Destination { get; set; }

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
