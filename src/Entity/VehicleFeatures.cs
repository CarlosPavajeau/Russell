﻿
namespace Entity
{
    public class VehicleFeatures
    {
        private byte _chairs;
        public VehicleFeatures(string type, string mark, string model, string modelNumber, string color, byte chairs)
        {
            Type = type;
            Mark = mark;
            Model = model;
            ModelNumber = modelNumber;
            Color = color;
            Chairs = chairs;
        }

        public VehicleFeatures()
        {

        }

        public string Type { get; set; }

        public string Mark { get; set; }

        public string Model { get; set; }

        public string ModelNumber { get; set; }

        public string Color { get; set; }

        public byte Chairs
        {
            get => _chairs;
            set => _chairs = (value > 0) ? value : throw new System.ArgumentException("The chairs value is invalid");
        }
    }
}
