using System;

namespace Entity
{
    public abstract class Delivery
    {
        Person sender, receiver, dispatcher;

        public Delivery(string number, Person sender, Person receiver, Person dispatcher)
        {
            Number = number;
            Sender = sender;
            Receiver = receiver;
            Dispatcher = dispatcher;
        }

        public string Number { get; }

        public Person Sender
        {
            get
            {
                return sender;
            }
            set
            {
                sender = value ?? throw new ArgumentException("The number value is invalid.");
            }
        }

        public Person Receiver
        {
            get
            {
                return receiver;
            }
            set
            {
                receiver = value ?? throw new ArgumentException("The number value is invalid.");
            }
        }

        public Person Dispatcher
        {
            get
            {
                return dispatcher;
            }
            set
            {
                dispatcher = value ?? throw new ArgumentException("The number value is invalid.");
            }
        }
    }
}
