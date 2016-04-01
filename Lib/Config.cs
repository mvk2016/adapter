using System;
using System.Configuration;

namespace AzureWSBridge.Lib
{
    class Config
    {
        public static string ReadSetting(string key)
        {
            var value = ConfigurationManager.AppSettings[key];
            if(value == null)
            {
                Console.WriteLine("Missing configuration key ", key);
                System.Environment.Exit(1);
            }
            return value;
        }
    }
}
