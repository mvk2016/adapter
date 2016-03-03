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
    
    static void Main()
    {
        var connector = WebSocketWrapper.Create(host);
        var request = new Requests();

        connector.OnConnect((WebSocketWrapper ws) => Console.WriteLine("Has connected"));
        connector.OnMessage((string message, WebSocketWrapper ws) => Console.WriteLine(request.ParseResponse(message)["messageType"]));
        connector.OnDisconnect((WebSocketWrapper ws) => Console.WriteLine("Has disconnected"));

        connector.Connect();

        // Wait until socket is no longer trying to connect
        while(connector.State().Equals(WebSocketState.Connecting))
        {
            Thread.Sleep(100);
        }

        Console.WriteLine("State: " + connector.State());

        var dict = new Dictionary<string, string>{
            ["username"] = "user@example.com",
            ["password"] = "password"
        };

        connector.SendMessage(request.MakeRequest("LoginRequest", dict));
        Console.ReadLine();
    }
}
