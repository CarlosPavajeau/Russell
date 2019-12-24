
namespace Entity
{
    [System.Serializable]
    public class VehicleFeature
    {
        private int _chairs;
        public VehicleFeature(string type, string mark, string model, string modelNumber, string color, byte chairs)
        {
            Type = type;
            Mark = mark;
            Model = model;
            ModelNumber = modelNumber;
            Color = color;
            Chairs = chairs;
        }

        public VehicleFeature()
        {

        }

        public string Type { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string ModelNumber { get; set; }

        public string Color { get; set; }

        public int Chairs
        {
            get => _chairs;
            set => _chairs = (value > 0) ? value : throw new System.ArgumentException("The chairs value is invalid");
        }
    }
}
