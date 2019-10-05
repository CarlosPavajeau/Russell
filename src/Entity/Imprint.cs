
namespace Entity
{
    public class Imprint
    {
        public Imprint(string engineNumber, string chassisNumber)
        {
            EngineNumber = engineNumber;
            ChassisNumber = chassisNumber;
        }

        public Imprint()
        {

        }

        public string EngineNumber { get; set; }

        public string ChassisNumber { get; set; }

    }
}
