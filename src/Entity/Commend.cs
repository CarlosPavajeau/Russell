using System;

namespace Entity
{
    public class Commend : Delivery
    {
        private decimal freightValue;
        private decimal agreement;

        public Commend(string number, Person sender, Person receiver, Person dispatcher, 
                       string description, decimal freightValue, decimal agreement, Vehicle vehicle) : base(number, sender, receiver, dispatcher)
        {
            Description = description;
            FreightValue = freightValue;
            Agreement = agreement;
            Vehicle = vehicle;
        }

        public string Description { get; set; }

        public decimal FreightValue
        {
            get
            {
                return freightValue;
            }
            set
            {
                freightValue = (value > 0) ? value : throw new ArgumentException("The freight value is invalid.");
            }
        }

        public decimal Agreement
        {
            get
            {
                return agreement;
            }
            set
            {
                agreement = (value > 0) ? value : throw new ArgumentException("The agreement is invalid.");
            }
        }

        public Vehicle Vehicle
        {
            get;
            set;
        }
    }
}
