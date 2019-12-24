using System;

namespace Entity
{
    [System.Serializable]
    public class Vehicle
    {
        public Vehicle(string licensePlate, string internalNumber, string propertyCardNumber, Employee owner, Employee driver)
        {
            LicensePlate = licensePlate;
            InternalNumber = internalNumber;
            PropertyCardNumber = propertyCardNumber;

            Feature = new VehicleFeature();
            Imprint = new Imprint();
            LegalInformation = new LegalInformation();

            Owner = owner;
            Driver = driver;
        }

        public LegalInformation LegalInformation { get; }

        public string LicensePlate { get; set; }

        public string InternalNumber { get; set; }

        public string PropertyCardNumber { get; set; }

        public bool State { get; set; }
        public Employee Owner { get; set; }
        public Employee Driver { get; set; }
        public Imprint Imprint { get; set; }
        public VehicleFeature Feature { get; set; }

        public void AddLegalInformation(LegalInformationType type, DateTime dueDate, DateTime dateOfRenovation)
        {
            LegalInformation.AddLegalInformation(type, dueDate, dateOfRenovation);
        }
    }
}
