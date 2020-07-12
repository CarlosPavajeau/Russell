using Common.Settings.Loaders;
using System.Net;

namespace Common.Settings
{
    public static class GeneralSettings
    {
        const string FILE_CONFIG = @"configuration.json";
        public const int ByteBufferSize = 8192;

        private static readonly SettingsLoader SettingsLoader = new SettingsLoader(FILE_CONFIG);

        public static IPAddress IPAddress => SettingsLoader.IPAddress;

        public static int Port => SettingsLoader.Port;

        public static int MaxClients => SettingsLoader.MaxClients;

        public static IPEndPoint IPEndPoint => new IPEndPoint(IPAddress, Port);

        public static string ConnectionString => SettingsLoader.ConnectionString;

        public static string SecretKey => SettingsLoader.SecretKey;
    }
}
