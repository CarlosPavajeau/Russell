
namespace Entity
{
    public class VehicleFeatures
    {
        public VehicleFeatures(string type, string mark, string model, string modelNumber, string color)
        {
            Type = type;
            Mark = mark;
            Model = model;
            ModelNumber = modelNumber;
            Color = color;
        }

        public VehicleFeatures()
        {

        }

        public string Type { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string ModelNumber { get; set; }

        public string Color { get; set; }
    }
}
