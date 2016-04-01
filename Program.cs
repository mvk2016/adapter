using System;
using AzureWSBridge.DataSources;
using AzureWSBridge.Lib;

namespace AzureWSBridge
{    
    public static class Program
    {
        public static void Main(string[] args)
        {
            /*
            new CirrusDataSource();
            Console.ReadLine();
            */

            Console.WriteLine(Config.ReadSetting("YanziHost"));
            Console.ReadLine();
        }

    }
}
