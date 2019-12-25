using Common;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Client
{
    public class Client
    {
        public Client()
        {
            ConnectedObject = new ConnectedObject(ConnectionHandler.CreateSocket());
        }

        public ConnectedObject ConnectedObject;

        public async Task<bool> Connect()
        {
            return await Task.Run(() => TryConnect());
        }

        private bool TryConnect()
        {
            try
            {
                ConnectedObject.Socket.Connect(ConnectionSettings.IPEndPoint);

                while (!ConnectedObject.Socket.Connected)
                    ConnectedObject.Socket.Connect(ConnectionSettings.IPEndPoint);

                return true;
            }
            catch (Exception)
            {
                return TryConnect();
            }
        }

        public async Task<ServerAnswer> RecieveServerAnswer()
        {
            return (await ReceiveObject()) is ServerAnswer serverAnswer ? serverAnswer : ServerAnswer.INVALID_COMMAND;
        }


        public async Task<object> ReceiveObject()
        {
            return await Task.Run(() => TryReceiveObject());
        }

        private object TryReceiveObject()
        {
            try
            {
                Message message = new Message();
                int bytesRead = ConnectedObject.Socket.Receive(message.ByteBuffer, SocketFlags.None);
                return (bytesRead > 0) ? Map.Deserialize(message) : null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> Send(TypeCommand typeCommand, TypeData typeData, object data)
        {
            Command command = GetCommand(typeCommand, typeData);

            DataPacket dataPacket = new DataPacket(command, data);

            return await Task.Run(() => TrySendObject(dataPacket));
        }

        private Command GetCommand(TypeCommand typeCommand, TypeData typeData)
        {
            switch (typeCommand)
            {
                case TypeCommand.SAVE:
                    return new SaveCommand(typeData);
                case TypeCommand.SEARCH:
                    return new SearchCommand(typeData);
                case TypeCommand.UPDATE:
                    return new UpdateCommand(typeData);
                case TypeCommand.DELETE:
                    return new DeleteCommand(typeData);
                default:
                    return null;
            }
        }

        public async Task<bool> Send(ClientRequest clientRequest)
        {
            return await Task.Run(() => TrySendObject(clientRequest));
        }
        private bool TrySendObject(object objectToSend)
        {
            try
            {
                ConnectedObject.Message = Map.Serialize(objectToSend);
                ConnectedObject.Socket.BeginSend(ConnectedObject.Message.ByteBuffer, 0, ConnectedObject.Message.ByteBuffer.Length, SocketFlags.None, new AsyncCallback(SendCallBack), ConnectedObject);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void SendCallBack(IAsyncResult result)
        {

        }
    }
}
