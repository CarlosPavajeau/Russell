
namespace Entity
{
    public enum TypeUser
    {
        SUPERUSER,
        ADMIN,
        DISPATCHER
    }
    public class User
    {
        public User(string user, string password, TypeUser typeUser)
        {
            AccessData = new AccessData(user, password);
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
