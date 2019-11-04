namespace Entity
{
    public class Employee : Person
    {
        public Employee(string id, string firstName, string secondName, string lastName, string secondLastName,
                        string cellphone, string email, string address) : base(id, firstName, secondName, lastName, secondLastName)
        {
            Cellphone = cellphone;
            Email = email;
            Address = address;
        }

        public string Cellphone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
