namespace Entity.Common
{
    public sealed class Message
    {
        public Message(byte[] data)
        {
            Data = data;
        }

        public Message()
        {
            Data = new byte[ConnectionSettings.ByteBufferSize];
        }
        public byte[] Data { get; set; }
    }
}
