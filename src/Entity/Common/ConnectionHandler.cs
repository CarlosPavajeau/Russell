using System.Net.Sockets;

namespace Entity.Common
{
    public static class ConnectionHandler
    {
        public static Socket CreateListener()
        {
            Socket socket;

            try
            {
                socket = CreateSocket();
                socket.Bind(ConnectionSettings.IPEndPoint);
                socket.Listen(ConnectionSettings.MAX_CLIENTS);
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
