using System;

namespace Entity
{
    public class BankDraft : Delivery
    {
        private decimal valueToSend;
        private decimal cost;

        public BankDraft(string number, DateTime date, Person sender, Person receiver, Person dispatcher, string destination,
                         decimal valueToSend, decimal cost, State state = State.ACTIVE) : base(number, date, sender,
                                                                                               receiver, dispatcher, destination, state)
        {
            ValueToSend = valueToSend;
            Cost = cost;
            Destination = destination;
        }

        public BankDraft(Person sender, Person receiver, Person dispatcher, string destination,
                         decimal valueToSend, decimal cost) : base(sender, receiver, dispatcher, destination)
        {
            ValueToSend = valueToSend;
            Cost = cost;
        }

        public decimal ValueToSend
        {
            get => valueToSend;
            set => valueToSend = (value > 0) ? value : throw new ArgumentException("The value to send is invalid");
        }

        public decimal Cost
        {
            get => cost;
            set => cost = (value >= 0) ? value : throw new ArgumentException("The cost is invalid");
        }
    }
}
