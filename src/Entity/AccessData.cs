using System;

namespace Entity
{
    public class AccessData
    {
        private string user;
        private string password;

        public AccessData(string user, string password)
        {

        }

        public string User
        {
            get
            {
                return user;
            }
            set
            {
                user = (value != string.Empty) ? value : throw new ArgumentException("The user is invalid");
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = (value != string.Empty) ? value : throw new ArgumentException("The password is invalid");
            }
        }
    }
}
