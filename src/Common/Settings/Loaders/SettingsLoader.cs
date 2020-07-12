using System.Collections.Generic;
using System.Net;

namespace Common.Settings.Loaders
{
    public class SettingsLoader
    {
        protected Dictionary<string, string> Settings { get; }

        public SettingsLoader(string fileName)
        {
            Settings = ConfigLoader.LoadConfigFromFile(fileName);
            Port = int.Parse(Settings["Port"]);
            MaxClients = int.Parse(Settings["LimitClients"]);
            IPAddress = IPAddress.Parse(Settings["IPAddress"]);
            SecretKey = Settings["SecretKey"];
        }

        public IPAddress IPAddress { get; }

        public int Port { get; }

        public int MaxClients { get; }

        public IPEndPoint IPEndPoint { get; }

        public string ConnectionString => Settings["ConnectionString"];
        public string SecretKey { get; }
    }
}
