using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

/// <summary>
/// Manages the workflow of data transmitting over a websocket to Cirrus
/// </summary>
public class YanziConnector
{
    static private string host = "wss://mqtt.yanzi.se:443/cirrusAPI";
    static private string username = "user@example.com";
    static private string password = "password";

    static private WebSocketWrapper connector;
    static private Requests request = new Requests();
    static private EventHubConnector eventHub = new EventHubConnector();

    static private bool shouldSubscribe = false;

    static void OnMessage(string message, WebSocketWrapper ws)
    {
        if (message == null || message == "")
            return;

        try
        {
            var type = request.ParseResponse<Response>(message).messageType;
            Console.WriteLine(type);

            switch (type)
            {
                case "LoginResponse":
                    var loginResponse = request.ParseResponse<LoginResponse>(message);

                    if (loginResponse.responseCode["name"] != "success")
                    {
                        Console.WriteLine("Login failed");
                        connector.Close();
                    }
                    else
                    {
                        Console.WriteLine("Login success");
                    }
                    break;

                case "GetLocationsResponse":
                    var locationResponse = request.ParseResponse<GetLocationsResponse>(message);

                    if (!shouldSubscribe) break;

                    foreach(var loc in locationResponse.list)
                    {
                        string locationId = loc.locationAddress["locationId"];
                        var subscribeRequest = new SubscribeRequest()
                        {
                            unitAddress = new { locationId = locationId },
                            subscriptionType = new {name = "default", resourceType = "SubscriptionType"}
                        };
                        Console.WriteLine(request.MakeRequest(subscribeRequest));
                        connector.SendMessage(request.MakeRequest(subscribeRequest));
                    }
                    break;

                case "SubscribeResponse":
                    break;

                case "SubscribeData":
                    Console.WriteLine(message);
                    eventHub.SendMessage(message);
                    break;
                default:
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(message);
            Console.WriteLine(e);
            connector.Close();
            return;
        }
    }

    /// <summary>
    /// Connects to Cirrus
    /// </summary>
    /// <returns>If the connection was successfully opened</returns>
    static bool Connect()
    {
        connector = WebSocketWrapper.Create(host);

        connector.OnConnect((WebSocketWrapper ws) => Console.WriteLine("Has connected"));
        connector.OnMessage(OnMessage);
        connector.OnDisconnect((WebSocketWrapper ws) => Console.WriteLine("Has disconnected"));

        connector.Connect();

        // Wait until socket is no longer trying to connect
        while (connector.State() == WebSocketState.Connecting)
            Thread.Sleep(100);

        if (connector.State() != WebSocketState.Open)
        {
            Console.WriteLine("Could not connect to Cirrus");
            return false;
        }
        return true;
    }

    static void Login()
    {
        var loginRequest = new LoginRequest()
        {
            username = username,
            password = password,
            timeSent = DateTime.Now
        };
        string req = request.MakeRequest(loginRequest);
        connector.SendMessage(req);
    }

    static void Main()
    {
        bool connected = Connect();
        if (!connected) return;

        Login();

        Console.ReadLine();
    }
}
