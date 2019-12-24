using Common;
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
                Log.PrintMsg($"Server starting....{DateTime.Now}");

                while (true)
                {
                    _connected.Reset();

                    _serverSocket.BeginAccept(new AsyncCallback(AcceptCallBack), _serverSocket);

                    _connected.WaitOne();
                }
            }
            catch (Exception exeption)
            {
                Log.PrintMsg(exeption);
            }
        }

        private void AcceptCallBack(IAsyncResult result)
        {
            _connected.Set();

            ConnectedObject ConnectedObject = new ConnectedObject(_serverSocket.EndAccept(result));
            Log.PrintMsg($"New ConnectedObject connect from {ConnectedObject.Socket.RemoteEndPoint}");
            _clients.Add(ConnectedObject);
            Log.PrintMsg($"Total clients: {_clients.Count}");
            BeginReceive(ConnectedObject);
        }

        private void BeginReceive(ConnectedObject ConnectedObject)
        {
            try
            {
                ConnectedObject.Socket.BeginReceive(ConnectedObject.Message.ByteBuffer, 0, ConnectionSettings.ByteBufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallBack), ConnectedObject);
            }
            catch (SocketException)
            {
                CloseClient(ConnectedObject);
            }
        }

        private void ReceiveCallBack(IAsyncResult result)
        {
            if (!CheckState(result, out string error, out ConnectedObject ConnectedObject))
            {
                Log.PrintMsg(error);
                return;
            }

            try
            {
                if (ConnectedObject.Socket.EndReceive(result) > 0)
                {
                    object receiveData = Map.Deserialize(ConnectedObject.Message);

                    if (receiveData is DataPacket data)
                    {
                        object datapacketProcesed = DataPacketHandler.HandleDataPacket(data);
                        SendReply(ConnectedObject, datapacketProcesed);
                    }
                    else if (receiveData is ClientRequest request)
                    {
                        Log.PrintMsg($"Receive client request: {request}, from {ConnectedObject.Socket.RemoteEndPoint}");
                        object clientrequestProcesed = ClientRequestHanlder.ProccessClientRequest(request);
                        SendReply(ConnectedObject, clientrequestProcesed);
                    }

                    ConnectedObject.Message.Clear();

                    BeginReceive(ConnectedObject);
                }
            }
            catch (SocketException exception)
            {
                Log.PrintMsg(exception);
                CloseClient(ConnectedObject);
                return;
            }
            catch (Exception exception)
            {
                Log.PrintMsg(exception);
                return;
            }
        }

        private bool CheckState(IAsyncResult result, out string error, out ConnectedObject ConnectedObject)
        {
            error = string.Empty;
            ConnectedObject = null;

            if (result is null)
            {
                error = "Async result null";
                return false;
            }

            ConnectedObject = result.AsyncState as ConnectedObject;

            if (ConnectedObject is null)
            {
                error = "Client null";
                return false;
            }

            return true;
        }

        public void SendReply(ConnectedObject ConnectedObject, object data)
        {
            if (ConnectedObject is null)
            {
                Log.PrintMsg("Unable to send reply: ConnectedObject null");
                return;
            }

            try
            {
                Message messageReply = Map.Serialize(data);

                ConnectedObject.Socket.BeginSend(messageReply.ByteBuffer, 0, messageReply.ByteBuffer.Length, SocketFlags.None, new AsyncCallback(SendReplyCallBack), ConnectedObject);

            }
            catch (SocketException exception)
            {
                Log.PrintMsg(exception);
                CloseClient(ConnectedObject);
            }
        }

        private void SendReplyCallBack(IAsyncResult result)
        {

        }

        private void CloseClient(ConnectedObject ConnectedObject)
        {
            Log.PrintMsg($"Client {ConnectedObject.Socket.RemoteEndPoint} disconect.");
            ConnectedObject.Close();
            _clients.Remove(ConnectedObject);
        }
    }
}
