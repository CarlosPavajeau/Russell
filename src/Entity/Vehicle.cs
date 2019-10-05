using System;
using System.Collections.Generic;

namespace Entity
{
    public class Vehicle
    {
        private int chairs;

        public Vehicle(string licensePlate, string internalNumber, string propertyCardNumber, Person owner,
                       Person driver, int chairs, Dictionary<string, string> vehicleFeatures)
        {
            LicensePlate = licensePlate;
            InternalNumber = internalNumber;
            PropertyCardNumber = propertyCardNumber;
            Chairs = chairs;

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

            Imprint.ChassisNumber = vehicleFeatures["ChassisNumber"];
            Imprint.EngineNumber = vehicleFeatures["EngineNumber"];
        }


        public LegalInformation LegalInformation { get; }

        public string LicensePlate { get; set; }

        public string InternalNumber { get; set; }

        public string PropertyCardNumber { get; set; }

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

        public bool Stade { get; set; }
        public Person Owner { get; set; }
        public Person Driver { get; set; }
        public Imprint Imprint { get; set; }
        public VehicleFeatures Features { get; set; }
    }
}
