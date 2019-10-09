
namespace Entity
{
    public enum TypeUser
    {
        SUPERUSER,
        ADMIN,
        DISPATCHER
    }
    public class User : Person
    {
        public User(AccessData accessData, TypeUser typeUser, string id, string firstName,
            string secondName, string lastName, string secondLastName, string address = "",
            string cellphone = "", string email = "") : base(id, firstName, secondName, lastName,
                                                            secondLastName, address, cellphone, email)
        {
            AccessData = accessData;
            TypeUser = typeUser;
        }

        public AccessData AccessData { get; set; }

        public TypeUser TypeUser { get; set; }

        public bool IsSuperUser()
        {
            return TypeUser == TypeUser.SUPERUSER;
        }

        public bool IsAdmin()
        {
            return TypeUser == TypeUser.ADMIN;
        }

        public bool IsDispatcher()
        {
            return TypeUser == TypeUser.DISPATCHER;
        }
    }
}
