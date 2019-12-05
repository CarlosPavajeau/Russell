namespace Entity
{
    [System.Serializable]
    public class AccessData
    {
        public AccessData(string user, string password)
        {
            User = user;
            Password = password;
        }

        public string User { get; set; }

        public string Password { get; set; }
    }
}
