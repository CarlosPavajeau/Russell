namespace Entity
{
    [System.Serializable]
    public class Route
    {
        public Route()
        {

        }

        public Route(string code, string originCity, string destinationCity, decimal cost)
        {
            Code = code;
            OriginCity = originCity;
            DestinationCity = destinationCity;
            Cost = cost;
        }

        public string Code { get; set; }

        public string OriginCity { get; set; }

        public string DestinationCity { get; set; }

        public decimal Cost { get; set; }
    }
}
