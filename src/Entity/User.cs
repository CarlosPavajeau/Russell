
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
        public User(AccessData accessData, TypeUser typeUser)
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
