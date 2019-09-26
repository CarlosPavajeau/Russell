using System;

namespace Entity
{
    public class Commend : Delivery
    {
        private string description;
        private decimal freightValue;
        private decimal agreement;

        public Commend(string number, Person sender, Person receiver, Person dispatcher, 
                       string description, decimal freightValue, decimal agreement) : base(number, sender, receiver, dispatcher)
        {
            Description = description;
            FreightValue = freightValue;
            Agreement = agreement;
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The description is invalid");
            }
        }

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
    }
}
