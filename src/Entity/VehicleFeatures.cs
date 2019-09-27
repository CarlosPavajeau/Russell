using System;

namespace Entity
{
    public class VehicleFeatures
    {
        private string type;
        private string mark;
        private string model;
        private string modelNumber;
        private string color;
        
        public VehicleFeatures(string type, string mark, string model, string modelNumber, string color)
        {
            Type = type;
            Mark = mark;
            Model = model;
            ModelNumber = modelNumber;
            Color = color;
        }

        public string Type
        {
            get
            {
                return type;
            }
            set
            {
                type = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The type is invalid");
            }
        }

        public string Mark
        {
            get
            {
                return mark;
            }
            set
            {
                mark = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The mark is invalid");
            }
        }

        public string Model
        {
            get
            {
                return model;
            }
            set
            {
                model = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The model is invalid");
            }
        }

        public string ModelNumber
        {
            get
            {
                return modelNumber;
            }
            set
            {
                modelNumber = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The model number is invalid");
            }
        }

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = (!string.IsNullOrEmpty(value)) ? value : throw new ArgumentException("The color is invalid");
            }
        }
    }
}
