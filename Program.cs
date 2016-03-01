using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// YanziConnector sets up a websocket to Yanzi Cirrus cloud, where requests and responses are sent.
/// </summary>
public class YanziConnector
{
    private string host;
    private ClientWebSocket socket;

    public YanziConnector()
    {
        Console.WriteLine("Creating Connector");
        host = "wss://mqtt.yanzi.se:443/cirrusAPI";
        socket = new ClientWebSocket();
    }

    static int getTime()
    {
        TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        return (int)span.TotalSeconds;
    }

    /// <summary>
    /// Opens a websocket connection to Cirrus
    /// </summary>
    /// <returns></returns>
    public void connect()
    {
        Console.WriteLine("Trying to connect");
        socket.ConnectAsync(new Uri(host, UriKind.Absolute), CancellationToken.None).Wait();
        Console.WriteLine(socket.State.ToString());
        Console.WriteLine("Has connected to Yanzi");
    }

    public void SendMessage(string s)
    {
        socket.SendAsync(new ArraySegment<Byte>(Encoding.Unicode.GetBytes(s)), WebSocketMessageType.Text, false, CancellationToken.None).Wait();
    }

    public void SendLoginRequest()
    {
        Console.WriteLine("Sending login request");
        String request = String.Format(
@"{{
    ""messageType"": ""LoginRequest""
    ""username"": ""username"",
    ""password"": ""password"",
    ""timeSent"": {0}
}}", getTime().ToString());
        SendMessage(request);
    }

    private async void Listen()
    {
        var buffer = new Byte[1000];
        WebSocketReceiveResult result;
        Console.WriteLine("Listening");
        while (true)
        {
            result = await socket.ReceiveAsync(new ArraySegment<Byte>(buffer), CancellationToken.None);

            var str = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine("{0} {1}", str.Length, str);
        }
    }

    public void StartListen()
    {
        Listen();
    }

    static void Main()
    {
        var connector = new YanziConnector();
        connector.connect();
        connector.StartListen();
        connector.SendLoginRequest();
        Thread.Sleep(10000);
    }
}
