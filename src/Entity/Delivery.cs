using System;

namespace Entity
{
    public enum Stade
    {
        ACTIVE,
        DELIVERED
    }
    public abstract class Delivery
    {
        private static int deliveryCount = 0;

        public Delivery(string number, DateTime date, Person sender, Person receiver, Person dispatcher, Stade stade)
        {
            Number = number;
            Date = date;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            Stade = stade;

        }

        public Delivery(Person sender, Person receiver, Person dispatcher, Stade stade = Stade.ACTIVE)
        {
            Number = (++deliveryCount).ToString("00000");
            Date = DateTime.Now;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
            Stade = stade;
        }

        public string Number { get; }

        public DateTime Date { get; }

        public Person Sender { get; set; }

        public Person Receiver { get; set; }

        public Person Dispatcher { get; set; }

        public Stade Stade { get; set; }

        public bool IsActived()
        {
            return Stade == Stade.ACTIVE;
        }

        public bool IsDelivered()
        {
            return Stade == Stade.DELIVERED;
        }
    }
}
