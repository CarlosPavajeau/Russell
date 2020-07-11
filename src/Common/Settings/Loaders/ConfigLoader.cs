using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Common.Settings.Loaders
{
    public static class ConfigLoader
    {
        const string FILE_LOG = @"config_loader_log.txt";

        public static Dictionary<string, string> LoadConfigFromFile(string filePath)
        {
            Dictionary<string, string> configuration = new Dictionary<string, string>();

            try
            {
                using FileStream fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read);
                using StreamReader reader = new StreamReader(fileStream);
                string data = reader.ReadToEnd();

                configuration = JsonSerializer.Deserialize(data, configuration.GetType()) as Dictionary<string, string>;
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }

            return configuration;
        }

        private static void LogException(Exception exception)
        {
            using FileStream fileStream = new FileStream(FILE_LOG, FileMode.Create, FileAccess.Write);
            using StreamWriter streamWriter = new StreamWriter(fileStream);
            streamWriter.WriteLine($"Exception message: {exception.Message}");
            streamWriter.WriteLine($"StackTrace: {exception.StackTrace}");
            streamWriter.WriteLine($"Data: {exception.Data}");
        }
    }
}
