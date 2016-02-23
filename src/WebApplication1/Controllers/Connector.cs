using System;
using System.Collections;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Summary description for YanziConnector
/// </summary>
public class YanziConnector
{
    private string host;
    private ClientWebSocket socket;
    public YanziConnector()
    {
        host = "wss://mqtt.yanzi.se:443/cirrusAPI";
        socket = new ClientWebSocket();
    }
    public async Task connect()
    {
        try
        {
            System.Diagnostics.Debug.WriteLine("Trying to connect");
            Console.WriteLine("Trying to connect");
            await socket.ConnectAsync(new Uri(host, UriKind.Absolute), new CancellationToken(true));
            System.Diagnostics.Debug.WriteLine("Has connected to Yanzi");
            Console.WriteLine("Has connected to Yanzi");
        }
        catch (WebSocketException e)
        {
            System.Diagnostics.Debug.WriteLine("Error: " + e.Message + " " + e.InnerException);
            Console.WriteLine("Error: " + e.Message + " " + e.InnerException);
            return;
        }
    }

    public async Task SendEmptyMessage()
    {
        await SendMessage(new Byte[0]);
    }

    public async Task SendMessage(Byte[] buffer)
    {
        await socket.SendAsync(new ArraySegment<Byte>(buffer), WebSocketMessageType.Binary, false, CancellationToken.None);
    }
    /*
    static void Main()
    {
        Console.WriteLine("Creating Connector");
        var connector = new YanziConnector();
        Task connectTask = connector.connect();
        connectTask.Wait();

        Console.WriteLine("Trying to send message");
        connectTask = connector.SendEmptyMessage();
        connectTask.Wait();
        Console.WriteLine("Has sent message");
    }*/
}
