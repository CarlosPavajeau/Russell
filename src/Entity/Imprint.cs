using System;

namespace Entity
{
    public class Imprint
    {
        private string engineNumber;
        private string chassisNumber;

        public Imprint(string engineNumber, string chassisNumber)
        {
            EngineNumber = engineNumber;
            ChassisNumber = chassisNumber;
        }

        public string EngineNumber
        {
            get
            {
                return engineNumber;
            }
            set
            {
                engineNumber = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The engine number is invalid");
            }
        }

        public string ChassisNumber
        {
            get
            {
                return chassisNumber;
            }
            set
            {
                chassisNumber = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The chassis number is invalid");
            }
        }

    }
}
