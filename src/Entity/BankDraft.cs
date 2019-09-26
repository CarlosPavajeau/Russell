using System;

namespace Entity
{
    public class BankDraft : Delivery
    {
        decimal valueToSend;
        decimal cost;
        string destination;

        public BankDraft(string number, Person sender, Person receiver, Person dispatcher, 
                         decimal valueToSend, decimal cost, string destination) : base(number, sender, receiver, dispatcher)
        {
            Date = DateTime.Now;
            ValueToSend = valueToSend;
            Cost = cost;
            Destination = destination;
        }

        public DateTime Date { get; }

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

        public string Destination
        {
            get
            {
                return destination;
            }
            set
            {
                destination = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The destination is invalid");
            }
        }
    }
}
