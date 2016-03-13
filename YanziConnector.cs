using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// Manages the workflow of data transmitting over a websocket to Cirrus
/// </summary>
public class YanziConnector
{
    private WebSocketWrapper connector;
    private JSONConverter json = new JSONConverter();

    void OnMessage(string message, WebSocketWrapper ws)
    {
        if (message == null || message == "")
            return;

        try
        {
            var type = json.ParseResponse<Response>(message).messageType;

            Response response = new Response();

            Type t = Type.GetType(type);

            if (t == null) t = typeof(Response);

            MethodInfo method = typeof(JSONConverter).GetMethod("ParseResponse");
            MethodInfo generic = method.MakeGenericMethod(t);
            response = generic.Invoke(json, new object[]{ message }) as Response;

            response.Action(this);
        }
        catch (Exception e)
        {
            Console.WriteLine(message);
            Console.WriteLine(e);
            Close();
            return;
        }
    }

    public void Close()
    {
        connector.Close();
    }

    /// <summary>
    /// Subscribes to given location
    /// </summary>
    /// <param name="locationId">ID of location to subscribe to</param>
    public void Subscribe(string locationId)
    {
        var subscribeRequest = new SubscribeRequest()
        {
            unitAddress = new { locationId = locationId },
            subscriptionType = new { name = "default", resourceType = "SubscriptionType" }
        };
        connector.SendMessage(json.MakeRequest(subscribeRequest));
    }
    /// <summary>
    /// Connects to Cirrus
    /// </summary>
    /// <returns>If the connection was successfully opened</returns>
    public bool Connect(string host)
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

    public void Login(string username, string password)
    {
        var loginRequest = new LoginRequest()
        {
            username = username,
            password = password,
            timeSent = DateTime.Now
        };
        string req = json.MakeRequest(loginRequest);
        connector.SendMessage(req);
    }
}
