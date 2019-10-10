using System;

namespace Entity
{
    public class BankDraft : Delivery
    {
        decimal valueToSend;
        decimal cost;

        public BankDraft(string number, DateTime date, Person sender, Person receiver, Person dispatcher,
                         decimal valueToSend, decimal cost, string destination, State state = State.ACTIVE) : base(number, date, sender, 
                                                                                                                   receiver, dispatcher, state)
        {
            ValueToSend = valueToSend;
            Cost = cost;
            Destination = destination;
        }

        public BankDraft(Person sender, Person receiver, Person dispatcher,
                         decimal valueToSend, decimal cost, string destination) : base(sender, receiver, dispatcher)
        {
            ValueToSend = valueToSend;
            Cost = cost;
            Destination = destination;
        }

        public decimal ValueToSend
        {
            get
            {
                return valueToSend;
            }
            set
            {
                valueToSend = (value > 0) ? value : throw new ArgumentException("The value to send is invalid");
            }
        }

        public decimal Cost
        {
            get
            {
                return cost;
            }
            set
            {
                cost = (value > 0) ? value : throw new ArgumentException("The cost is invalid");
            }
        }

        public string Destination { get; set; }
    }
}
