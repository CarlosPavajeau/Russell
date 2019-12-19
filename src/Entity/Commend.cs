using System;

namespace Entity
{
    public class Commend : Delivery
    {
        private decimal freightValue;
        private decimal agreement;

        public Commend(string number, DateTime date, Person sender, Person receiver, AdministrativeEmployee dispatcher, string destination, string description,
                       decimal freightValue, decimal agreement, Vehicle vehicle, State state) : base(number, date, sender,
                                                                                                                    receiver, dispatcher, destination, state)
        {
            Description = description;
            FreightValue = freightValue;
            Agreement = agreement;
            Vehicle = vehicle;
            Total = FreightValue + Agreement;
        }

        public Commend(string number, Person sender, Person receiver, AdministrativeEmployee dispatcher, string destination, string desciption,
                       decimal freightValue, decimal agreement, Vehicle vehicle, State state = State.ACTIVE) : this(number, DateTime.Now, sender, receiver, dispatcher, destination, desciption, freightValue, agreement, vehicle, state)
        {

        }

        public string Description { get; set; }

        public decimal FreightValue
        {
            get => freightValue;
            set => freightValue = (value >= 0) ? value : throw new ArgumentException("The freight value is invalid.");
        }

        public decimal Agreement
        {
            get => agreement;
            set => agreement = (value >= 0) ? value : throw new ArgumentException("The agreement is invalid.");
        }

        public Vehicle Vehicle
        {
            get;
            set;
        }
    }
}
