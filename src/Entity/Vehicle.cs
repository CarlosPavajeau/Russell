using System;

namespace Entity
{
    public class Vehicle
    {
        private string licensePlate;
        private string internalNumber;
        private string propertyCardNumber;

        LegalInformation license;
        LegalInformation soat;
        LegalInformation operationCard;
        LegalInformation technoMechanicalReview;
        LegalInformation biMonthlyReview;

        int chairs;

        public string LicensePlate
        {
            get
            {
                return licensePlate;
            }
            set
            {
                licensePlate = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The license palte is invalid");
            }
        }

        public string InternalNumber
        {
            get
            {
                return internalNumber;
            }
            set
            {
                internalNumber = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The internal number is invalid");
            }
        }

        public string PropertyCardNumber
        {
            get
            {
                return propertyCardNumber;
            }
            set
            {
                propertyCardNumber = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The property card number is invalid");
            }
        }

        public int Chairs
        {
            get
            {
                return chairs;
            }
            set
            {
                chairs = (value > 0) ? value : throw new ArgumentException("The chairs value is invalid"); 
            }
        }

        public bool Stade
        {
            get;
            set;
        }

        public Person Owner
        {
            get;
            set;
        }

        public Person Driver
        {
            get;
            set;
        }

        public Imprint Imprint
        {
            get;
            set;
        }

        public VehicleFeatures Features
        {
            get;
            set;
        }

    }
}
