namespace Entity
{
    public class AdministrativeEmployee : Employee
    {
        public AdministrativeEmployee(string id, string firstName, string secondName, string lastName, string secondLastName,
                                      string cellphone, string email, string address, User user) : base(id, firstName, secondName, lastName, secondLastName,
                                                                                                        cellphone, email, address)
        {
            User = user;
        }

        public User User { get; set; }
    }
}
