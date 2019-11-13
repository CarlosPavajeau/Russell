using System;
using System.Collections.Generic;

namespace Entity
{
    public class Vehicle
    {
        public Vehicle(string licensePlate, string internalNumber, string propertyCardNumber, Employee owner,
                       Employee driver, Dictionary<string, string> vehicleFeatures)
        {
            LicensePlate = licensePlate;
            InternalNumber = internalNumber;
            PropertyCardNumber = propertyCardNumber;

            Features = new VehicleFeatures();
            Imprint = new Imprint();
            LegalInformation = new LegalInformation();

            SetFeatures(vehicleFeatures);

            Owner = owner;
            Driver = driver;
        }

        private void SetFeatures(Dictionary<string, string> vehicleFeatures)
        {
            Features.Color = vehicleFeatures["Color"];
            Features.Mark = vehicleFeatures["Mark"];
            Features.Model = vehicleFeatures["Model"];
            Features.ModelNumber = vehicleFeatures["ModelNumber"];
            Features.Type = vehicleFeatures["Type"];
            Features.Chairs = byte.Parse(vehicleFeatures["Chairs"]);

            Imprint.ChassisNumber = vehicleFeatures["ChassisNumber"];
            Imprint.EngineNumber = vehicleFeatures["EngineNumber"];
        }


        public LegalInformation LegalInformation { get; }

        public string LicensePlate { get; set; }

        public string InternalNumber { get; set; }

        public string PropertyCardNumber { get; set; }

        public bool Stade { get; set; }
        public Employee Owner { get; set; }
        public Employee Driver { get; set; }
        public Imprint Imprint { get; set; }
        public VehicleFeatures Features { get; set; }

        public void AddLegalInformation(LegalInformationType type, DateTime dueDate, DateTime dateOfRenovation)
        {
            LegalInformation.AddLegalInformation(type, dueDate, dateOfRenovation);
        }
    }
}
