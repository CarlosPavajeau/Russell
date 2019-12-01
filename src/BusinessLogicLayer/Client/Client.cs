using Entity.Common;
using System;
using System.Net.Sockets;
using System.Threading;

namespace BusinessLogicLayer.Client
{
    public class Client
    {
        public delegate void ProcessReceiveData(object data);
        public delegate void ProcessServerAnswer(ServerAnswer answer);

        public ProcessReceiveData ReceiveData;
        public ProcessServerAnswer ServerAnswer;

        public Client()
        {

        }

        public ConnectedObject client;

        public void Connect()
        {
            client = new ConnectedObject(ConnectionHandler.CreateSocket());

            while (!client.Socket.Connected)
            {
                try
                {
                    client.Socket.Connect(ConnectionSettings.IPEndPoint);
                }
                catch (SocketException)
                {

                }
            }

            Thread receiveThread = new Thread(() => Receive());
        }

        private void Receive()
        {
            while (true)
            {
                try
                {
                    Message message = new Message();
                    int bytesRead = client.Socket.Receive(message.ByteBuffer, SocketFlags.None);
                    if (bytesRead > 0)
                    {
                        object receiveData = Map.Deserialize(message);

                        if (receiveData is ServerAnswer answer)
                            ServerAnswer?.Invoke(answer);
                        else
                            ReceiveData?.Invoke(receiveData);
                    }
                }
                catch (SocketException)
                {
                    client.Close();
                    Thread.CurrentThread.Abort();
                    Connect();
                }
                catch (Exception)
                {

                }


            }
        }

        public void Send(object objectToSend)
        {
            Thread thread = new Thread(() => StartSend(objectToSend));
            thread.Start();
        }

        private void StartSend(object objectToSend)
        {
            client.Message = Map.Serialize(objectToSend);

            try
            {
                client.Socket.BeginSend(client.Message.ByteBuffer, 0, client.Message.ByteBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), client);
            }
            catch (SocketException)
            {

            }
        }

        private void SendCallBack(IAsyncResult result)
        {

        }
    }
}
