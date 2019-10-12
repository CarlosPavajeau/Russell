using System.Collections.Generic;

namespace Entity
{
    public class Person
    {
        public Person(string id)
        {
            ID = id;
        }

        public Person(string id, string firstName, string secondName, string lastName,
                      string secondLastName, string address = "", string cellphone = "", string email = "")
        {
            ID = id;
            FirstName = firstName;
            SecondName = secondName;
            LastName = lastName;
            SecondLastName = secondLastName;
            Address = address;
            Cellphone = cellphone;
            Email = email;
        }

        public string ID { get; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string LastName { get; set; }

        public string SecondLastName { get; set; }
        public string Name => $"{FirstName} {SecondName} {LastName} {SecondLastName}";

        public string Address { get; set; }

        public string Cellphone { get; set; }

        public string Email { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is Person person)
                return person.ID == ID;

            return false;
        }

        public override int GetHashCode()
        {
            return 1213502048 + EqualityComparer<string>.Default.GetHashCode(ID);
        }

        public static bool operator ==(Person left, Person right)
        {
            return left is null || right is null ? false : left.Equals(right);
        }

        public static bool operator !=(Person left, Person right)
        {
            return !(left == right);
        }

    }
}
