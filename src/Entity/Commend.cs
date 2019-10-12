using System;

namespace Entity
{
    public class Commend : Delivery
    {
        private decimal freightValue;
        private decimal agreement;

        public Commend(string number, DateTime date, Person sender, Person receiver, Person dispatcher, string description,
                       decimal freightValue, decimal agreement, Vehicle vehicle, State state = State.ACTIVE) : base(number, date, sender,
                                                                                                                    receiver, dispatcher, state)
        {
            Description = description;
            FreightValue = freightValue;
            Agreement = agreement;
            Vehicle = vehicle;
        }

        public Commend(Person sender, Person receiver, Person dispatcher, string desciption,
                       decimal freightValue, decimal agreement, Vehicle vehicle) : base(sender, receiver, dispatcher)
        {
            Description = desciption;
            FreightValue = freightValue;
            Agreement = agreement;
            Vehicle = vehicle;
        }

        public string Description { get; set; }

        public decimal FreightValue
        {
            get => freightValue;
            set => freightValue = (value > 0) ? value : throw new ArgumentException("The freight value is invalid.");
        }

        public decimal Agreement
        {
            get => agreement;
            set => agreement = (value > 0) ? value : throw new ArgumentException("The agreement is invalid.");
        }

        public Vehicle Vehicle
        {
            get;
            set;
        }
    }
}
