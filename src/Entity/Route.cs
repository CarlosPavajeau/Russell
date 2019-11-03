namespace Entity
{
    public class Route
    {
        public Route()
        {

        }

        public Route(string originCity, string destinationCity, decimal cost)
        {
            OriginCity = originCity;
            DestinationCity = destinationCity;
            Cost = cost;
        }

        public string OriginCity { get; set; }

        public string DestinationCity { get; set; }

        public decimal Cost { get; set; }
    }
}
