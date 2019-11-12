using System.Net;
using System.IO;

namespace Entity.Common
{
    public static class ConnectionSettings
    {
        const string FILE_CONFIG = @"conn_config.conf";

        const string FILE_LOG = @"log.txt";

        const string COMMENT = "#";

        const char DELIMITER = '=';

        public static IPAddress IPAddress { get => _connectionSettings.IPAddress; }

        public static int Port { get => _connectionSettings.Port; }

        public static int MaxClients { get => _connectionSettings.MaxClients; }

        public static IPEndPoint IPEndPoint => new IPEndPoint(IPAddress, Port);

        public static int ByteBufferSize => 1024;

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
                try
                {
                    using (FileStream fileStream = new FileStream(FILE_CONFIG, FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader streamReader = new StreamReader(fileStream))
                        {
                            while (!streamReader.EndOfStream)
                            {
                                string data = streamReader.ReadLine();

                                if (data.StartsWith(COMMENT))
                                    continue;

                                ProccessData(data);
                            }
                        }
                    }
                }
                catch (IOException exception)
                {
                    using (FileStream fileStream = new FileStream(FILE_LOG, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(fileStream))
                        {
                            streamWriter.WriteLine($"Exception message: {exception.Message}");
                            streamWriter.WriteLine($"StackTrace: {exception.StackTrace}");
                            streamWriter.WriteLine($"Data: {exception.Data}");
                        }
                    }
                    throw;
                }
            }

            private void ProccessData(string data)
            {
                string[] config = data.Split(DELIMITER);

                switch (config[0])
                {
                    case "IPAddress":
                        {
                            IPAddress = IPAddress.Parse(config[1]);
                            break;
                        }
                    case "Port":
                        {
                            Port = int.Parse(config[1]);
                            break;
                        }
                    case "MAX_CLIENTS":
                        {
                            MaxClients = int.Parse(config[1]);
                            break;
                        }
                }
            }
        }
    }
}
