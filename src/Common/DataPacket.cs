using System;

namespace Common
{
    [Serializable]
    public sealed class DataPacket
    {
        public DataPacket(Command command, object data)
        {
            Command = command;
            Data = data;
        }

        public Command Command { get; set; }

        public object Data { get; set; }
    }
}
