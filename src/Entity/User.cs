using System;

namespace Entity
{
    public enum TypeUser
    {
        SuperUser,
        Administrator,
        Dispatcher
    }

    [Serializable]
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
            return TypeUser == TypeUser.SuperUser;
        }

        public bool IsAdmin()
        {
            return TypeUser == TypeUser.Administrator;
        }

        public bool IsDispatcher()
        {
            return TypeUser == TypeUser.Dispatcher;
        }
    }
}
