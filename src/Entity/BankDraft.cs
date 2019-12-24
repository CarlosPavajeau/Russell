using System;

namespace Entity
{
    [Serializable]
    public class BankDraft : Delivery
    {
        private decimal valueToSend;
        private decimal cost;

        public BankDraft(string number, DateTime date, Person sender, Person receiver, AdministrativeEmployee dispatcher, string destination,
                         decimal valueToSend, decimal cost, State state) : base(number, date, sender,
                                                                                               receiver, dispatcher, destination, state)
        {
            ValueToSend = valueToSend;
            Cost = cost;
            Total = ValueToSend + Cost;
        }

        public BankDraft(string number, Person sender, Person receiver, AdministrativeEmployee dispatcher, string destination,
                         decimal valueToSend, decimal cost, State state = State.ACTIVE) : this(number, DateTime.Now, sender, receiver, dispatcher, destination, valueToSend, cost, state)
        {

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
