using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Manages the workflow of datatransmitting over a websocket to Cirrus
/// </summary>
public class YanziConnector
{
    static string host = "wss://mqtt.yanzi.se:443/cirrusAPI";
    static string username = "user@example.com";
    static string password = "password";

    static WebSocketWrapper connector;
    static Requests request;

    static void OnMessage(string message, WebSocketWrapper ws)
    {
        Console.WriteLine(request.ParseResponse(message)["messageType"]);
    }

    static void Main()
    {
        connector = WebSocketWrapper.Create(host);
        request = new Requests();

        connector.OnConnect((WebSocketWrapper ws) => Console.WriteLine("Has connected"));
        connector.OnMessage(OnMessage);
        connector.OnDisconnect((WebSocketWrapper ws) => Console.WriteLine("Has disconnected"));

        connector.Connect();

        // Wait until socket is no longer trying to connect
        while(connector.State().Equals(WebSocketState.Connecting))
        {
            Thread.Sleep(100);
        }

        Console.WriteLine("State: " + connector.State());

        var dict = new Dictionary<string, string>{
            ["username"] = username,
            ["password"] = password
        };

        connector.SendMessage(request.MakeRequest("LoginRequest", dict));
        Console.ReadLine();
    }
}
