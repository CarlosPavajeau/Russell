using Common.Settings;
using System.Net.Sockets;

namespace Common
{
    public static class ConnectionHandler
    {
        public static Socket CreateListener()
        {
            Socket socket;

            try
            {
                socket = CreateSocket();
                socket.Bind(GeneralSettings.IPEndPoint);
                socket.Listen(GeneralSettings.MaxClients);
            }
            catch (System.Exception)
            {
                throw;
            }

            return socket;
        }

        public static Socket CreateSocket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
