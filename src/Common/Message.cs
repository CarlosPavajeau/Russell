using Common.Settings;

namespace Common
{
    public sealed class Message
    {
        public Message(byte[] byteBuffer)
        {
            ByteBuffer = byteBuffer;
        }

        public Message()
        {
            ByteBuffer = new byte[GeneralSettings.ByteBufferSize];
        }
        public byte[] ByteBuffer { get; set; }

        public void Clear()
        {
            ByteBuffer = new byte[GeneralSettings.ByteBufferSize];
        }
    }
}
