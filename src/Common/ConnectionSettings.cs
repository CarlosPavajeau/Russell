using System.Collections.Generic;
using System.Net;

namespace Common
{
    public static class ConnectionSettings
    {
        const string FILE_CONFIG = @"conn_config.conf";

        public const int ByteBufferSize = 8192;

        public static IPAddress IPAddress { get => _connectionSettings.IPAddress; }

        public static int Port { get => _connectionSettings.Port; }

        public static int MaxClients { get => _connectionSettings.MaxClients; }

        public static IPEndPoint IPEndPoint => new IPEndPoint(IPAddress, Port);

        private static _ConnectionSettings _connectionSettings = new _ConnectionSettings();

        private sealed class _ConnectionSettings
        {
            public _ConnectionSettings()
            {
                LoadConfigFromFile();
            }

            public int Port { get; private set; }

            public int MaxClients { get; private set; }

            public IPAddress IPAddress { get; private set; }

            private void LoadConfigFromFile()
            {
                Dictionary<string, string> config = ConfigLoader.LoadConfigFromFile(FILE_CONFIG);

                Port = int.Parse(config["Port"]);
                MaxClients = int.Parse(config["LimitClients"]);
                IPAddress = IPAddress.Parse(config["IPAddress"]);
            }
        }
    }
}
