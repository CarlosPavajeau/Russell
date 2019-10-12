using System.Collections.Generic;

namespace Entity
{
    public class AccessData
    {
        public AccessData(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; set; }

        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj is AccessData accessData)
                return User == accessData.User && Password == accessData.Password;

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = -1879510246;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(User);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }

        public static bool operator ==(AccessData left, AccessData right)
        {
            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(AccessData left, AccessData right)
        {
            return !(left == right);
        }
    }
}
