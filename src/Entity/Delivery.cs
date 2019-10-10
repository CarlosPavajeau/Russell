using System;

namespace Entity
{
    public enum State
    {
        ACTIVE,
        DELIVERED
    }
    public abstract class Delivery
    {
        private static int deliveryCount = 0;

        public Delivery(string number, DateTime date, Person sender, Person receiver, Person dispatcher, State state)
        {
            Number = number;
            Date = date;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            State = state;

        }

        public Delivery(Person sender, Person receiver, Person dispatcher, State state = State.ACTIVE)
        {
            Number = (++deliveryCount).ToString("00000");
            Date = DateTime.Now;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            State = state;
        }

        public string Number { get; }

        public DateTime Date { get; }

        public Person Sender { get; set; }

        public Person Receiver { get; set; }

        public Person Dispatcher { get; set; }

        public State State { get; set; }

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
