using System;
using System.Collections.Generic;
using System.IO;

namespace Common
{
    public static class ConfigLoader
    {
        const string COMMENT = "#";
        const char DELIMITER = '=';
        const string FILE_LOG = @"config_loader_log.txt";

        public static Dictionary<string, string> LoadConfigFromFile(string filePath)
        {
            Dictionary<string, string> configuration = new Dictionary<string, string>();

            try
            {
                using (FileStream fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string data = reader.ReadLine();

                            if (IsComment(data) || data.Length == 0)
                                continue;

                            data = data.Trim();
                            string[] config = data.Split(DELIMITER);
                            configuration.Add(config[0], config[1]);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }

            return configuration;
        }

        public static string LoadDBConnectionString(string filePath)
        {
            string dbConnectionstr = "";
            try
            {
                using (FileStream fileStream = new FileStream(@filePath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string data = reader.ReadLine();

                            if (IsComment(data) || data.Length == 0)
                                continue;

                            dbConnectionstr = data.Substring(17);
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }

            return dbConnectionstr;
        }

        private static void LogException(Exception exception)
        {
            using (FileStream fileStream = new FileStream(FILE_LOG, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine($"Exception message: {exception.Message}");
                    streamWriter.WriteLine($"StackTrace: {exception.StackTrace}");
                    streamWriter.WriteLine($"Data: {exception.Data}");
                }
            }
        }

        private static bool IsComment(string str)
        {
            return str.StartsWith(COMMENT);
        }
    }
}
