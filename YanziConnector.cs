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
    static string host = "wss://mqtt.yanzi.se:443/cirrusAPI";
    static string username = "user@example.com";
    static string password = "password";

    static WebSocketWrapper connector = WebSocketWrapper.Create(host);
    static Requests request = new Requests();
    static EventHubConnector eventHub = new EventHubConnector();

    static bool shouldSubscribe = false;

    static void OnMessage(string message, WebSocketWrapper ws)
    {
        if (message == null || message == "")
            return;

        try
        {
            var type = request.ParseResponse<JSONMessage.Response>(message).messageType;
            Console.WriteLine(type);

            switch (type)
            {
                case "LoginResponse":
                    var loginResponse = request.ParseResponse<JSONMessage.LoginResponse>(message);

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
                    var locationResponse = request.ParseResponse<JSONMessage.GetLocationsResponse>(message);

                    if (!shouldSubscribe) break;

                    foreach(var loc in locationResponse.list)
                    {
                        string locationId = loc.locationAddress["locationId"];
                        var subscribeRequest = new JSONMessage.SubscribeRequest()
                        {
                            unitAddress = new Dictionary<string, string>{["locationId"] = locationId },
                            subscriptionType = new Dictionary<string, string> { ["name"] = "default", ["resourceType"] = "SubscriptionType" }
                        };
                        connector.SendMessage(request.MakeRequest(subscribeRequest));
                    }
                    break;

                case "SubscribeResponse":
                    //Console.WriteLine(message);
                    break;

                case "SubscribeData":
                    //Console.WriteLine(message);
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

    static void Main()
    {
        connector.OnConnect((WebSocketWrapper ws) => Console.WriteLine("Has connected"));
        connector.OnMessage(OnMessage);
        connector.OnDisconnect((WebSocketWrapper ws) => Console.WriteLine("Has disconnected"));

        connector.Connect();

        // Wait until socket is no longer trying to connect
        while (connector.State() == WebSocketState.Connecting)
            Thread.Sleep(100);

        if(connector.State() != WebSocketState.Open)
        {
            Console.WriteLine("Could not connect to Cirrus");
            return;
        }

        var loginRequest = new JSONMessage.LoginRequest()
        {
            username = username,
            password = password,
            timeSent = DateTime.Now
        };
        string req = request.MakeRequest(loginRequest);
        connector.SendMessage(req);

        Console.ReadLine();
    }
}
