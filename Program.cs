using System;
using AzureWSBridge.DataSources;

namespace AzureWSBridge
{    
    public class Program
    {
        static void Main()
        {
            new CirrusDataSource();
            Console.ReadLine();
        }

    }
}
