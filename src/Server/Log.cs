using System;
using System.IO;

namespace Server
{
    public static class Log
    {
        const string LOG_FILE = "ServerLog.txt";

        public static void PrintMsg(string msg)
        {
            Console.WriteLine(msg);
            SaveLog(msg);
        }

        public static void PrintMsg(Exception exception)
        {
            PrintMsg($"Exception message: {exception.Message}");
            PrintMsg($"StackTrace: {exception.StackTrace}");
        }

        private static async void SaveLog(string msg)
        {
            using FileStream fileStream = new FileStream(@LOG_FILE, FileMode.Append, FileAccess.Write);
            using StreamWriter streamWriter = new StreamWriter(fileStream);
            await streamWriter.WriteLineAsync(msg);
        }
    }
}
