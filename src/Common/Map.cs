using Common.Settings;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Common
{
    public static class Map
    {
        private static byte[] SecretKey => Convert.FromBase64String(GeneralSettings.SecretKey);
        public static Message Serialize(object anObject)
        {
            using MemoryStream memoryStream = new MemoryStream();
            new BinaryFormatter().Serialize(memoryStream, anObject);

            return new Message(Encrypt(memoryStream.ToArray()));
        }

        public static object Deserialize(Message message)
        {
            using MemoryStream memoryStream = new MemoryStream(Decrypt(message.ByteBuffer));
            return new BinaryFormatter().Deserialize(memoryStream);
        }

        private static byte[] Encrypt(byte[] data)
        {
            AesManaged aesManaged = CreateAesManaged();

            using ICryptoTransform cryptoTransform = aesManaged.CreateEncryptor();
            using MemoryStream memoryStream = new MemoryStream();
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write);
            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.FlushFinalBlock();

            return memoryStream.ToArray();
        }

        private static byte[] Decrypt(byte[] data)
        {
            AesManaged aesManaged = CreateAesManaged();

            using ICryptoTransform cryptoTransform = aesManaged.CreateDecryptor();
            using MemoryStream memoryStream = new MemoryStream(data);
            using CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read);

            byte[] decryptData = new byte[data.Length];
            cryptoStream.Read(decryptData, 0, decryptData.Length);

            return decryptData;
        }

        private static AesManaged CreateAesManaged()
        {
            AesManaged aesManaged = new AesManaged()
            {
                Mode = CipherMode.CBC,
                Padding = PaddingMode.Zeros,
                KeySize = 0x80,
            };

            aesManaged.Key = SecretKey;
            aesManaged.IV = SecretKey;

            return aesManaged;
        }
    }
}
