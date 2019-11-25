using Entity.Common;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Server
{
    public sealed class Server
    {
        private Socket _serverSocket;
        private List<ConnectedObject> _clients;
        private static ManualResetEvent _connected = new ManualResetEvent(false);

        public Server()
        {
            _serverSocket = ConnectionHandler.CreateListener();
            _clients = new List<ConnectedObject>();
        }

        public void StartListening()
        {
            try
            {
                while (true)
                {
                    _connected.Reset();

                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), _serverSocket);
                }
            }
            catch (Exception exeption)
            {
                Log.PrintMsg(exeption.Message);
            }
        }

        private void AcceptCallBack(IAsyncResult result)
        {
            _connected.Set();

            ConnectedObject client = new ConnectedObject(_serverSocket.EndAccept(result));
            Log.PrintMsg($"New client connect from {client.Socket.LocalEndPoint.ToString()}");
            _clients.Add(client);
            BeginReceive(client);
        }

        private void ReceiveCallBack(IAsyncResult result)
        {
            if (!CheckState(result, out string error, out ConnectedObject client))
            {
                Log.PrintMsg(error);
                return;
            }

            int bytesRead;
            try
            {
                bytesRead = client.Socket.EndReceive(result);
            }
            catch (SocketException)
            {
                CloseClient(client);
                return;
            }
            catch (Exception exception)
            {
                Log.PrintMsg(exception.Message);
                return;
            }

            if (bytesRead > 0)
            {
                object receiveData = Map.Deserialize(client.Message);

                if (receiveData is DataPacket data)
                {

                }
                else if (receiveData is ClientRequest request)
                {

                }

                client.Message.Clear();
            }

            BeginReceive(client);
        }

        private void BeginReceive(ConnectedObject client)
        {
            try
            {
                client.Socket.BeginReceive(client.Message.ByteBuffer, 0, ConnectionSettings.ByteBufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallBack), client);
            }
            catch (SocketException)
            {
                CloseClient(client);
            }
        }

        private bool CheckState(IAsyncResult result, out string error, out ConnectedObject client)
        {
            error = string.Empty;
            client = null;

            if (result is null)
            {
                error = "Async result null";
                return false;
            }

            client = result.AsyncState as ConnectedObject;

            if (client is null)
            {
                error = "Client null";
                return false;
            }

            return true;
        }

        public void SendReply(ConnectedObject client, object data)
        {
            if (client is null)
            {
                Log.PrintMsg("Unable to send reply: client null");
                return;
            }

            try
            {
                Message messageReply = Map.Serialize(data);

                client.Socket.BeginSend(messageReply.ByteBuffer, 0, messageReply.ByteBuffer.Length, SocketFlags.None, new AsyncCallback(SendReplyCallBack), client);

            }
            catch (SocketException)
            {
                CloseClient(client);
            }
        }

        private void SendReplyCallBack(IAsyncResult result)
        {

        }

        private void CloseClient(ConnectedObject client)
        {
            client.Close();
            _clients.Remove(client);
            Log.PrintMsg("Client disconnect");
        }
    }
}
