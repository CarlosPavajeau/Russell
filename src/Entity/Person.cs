namespace Entity
{
    [System.Serializable]
    public class Person
    {
        public Person(string id, string firstName, string secondName, string lastName, string secondLastName)
        {
            ID = id;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            SecondLastName = secondLastName;
        }

        public string ID { get; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string SecondLastName { get; set; }
        public string Name => $"{FirstName} {SecondName} {LastName} {SecondLastName}";
    }
}
