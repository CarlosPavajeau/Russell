using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Entity.Common
{
    public static class Map
    {
        public static Message Serialize(object anObject)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(memoryStream, anObject);
                return new Message(memoryStream.ToArray());
            }
        }

        public static object Deserialize(Message message)
        {
            using (MemoryStream memoryStream = new MemoryStream(message.Data))
                return new BinaryFormatter().Deserialize(memoryStream);
        }
    }
}
