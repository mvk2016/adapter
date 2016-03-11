using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MainClass
{
    static private string host = "wss://mqtt.yanzi.se:443/cirrusAPI";
    static private string username = "user@example.com";
    static private string password = "password";
    static private string location = "871073";
    static private bool shouldSubscribe = true;

    static YanziConnector yanzi = new YanziConnector();

    static void Main()
    {
        bool connected = yanzi.Connect(host);
        if (!connected) return;

        yanzi.Login(username, password);

        if (shouldSubscribe)
            yanzi.Subscribe(location);

        Console.ReadLine();
    }
}
