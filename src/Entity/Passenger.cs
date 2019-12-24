namespace Entity
{
    [System.Serializable]
    public class Passenger : Person
    {
        public Passenger(string id, string firstName, string secondName, string lastName, string secondLastName) :
                         base(id, firstName, secondName, lastName, secondLastName)
        {

        }
    }
}
