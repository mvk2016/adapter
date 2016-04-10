using System;
using System.Net.WebSockets;
using System.Threading;
using AzureWSBridge.Lib;
using AzureWSBridge.Requests;
using AzureWSBridge.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AzureWSBridge.DataSources
{
    /// <summary>
    /// Manages the workflow of data transmitting over a websocket to Cirrus
    /// </summary>
    public class CirrusDataSource
    {
        private WebSocketWrapper socket;

        public CirrusDataSource()
        {
            if (!Connect()) return;

            Login();

            Subscribe();
        }

        void OnMessage(string message, WebSocketWrapper ws)
        {
            Console.WriteLine(message);
            if (message == null || message == "")
                return;
            
            try
            {
                // json.ParseMessage<Response>(message).Action();

                /*var type = json.ParseMessage<Response>(message).messageType;

            Response response = new Response();

            Type t = Type.GetType(type);

            if (t == null) t = typeof(Response);
           
            MethodInfo method = typeof(JSONConverter).GetMethod("ParseResponse");
            MethodInfo generic = method.MakeGenericMethod(t);
            response = generic.Invoke(json, new object[]{ message }) as Response;

            response.Action();*/
            }
            catch (Exception e)
            {
                Console.WriteLine(message);
                Console.WriteLine(e);
                Close();
                return;
            }
        }

        /// <summary>
        /// Subscribes to given location
        /// </summary>
        /// <param name="locationId">ID of location to subscribe to</param>
        public void Subscribe()
        {
            var subscribeRequest = new SubscribeRequest()
            {
                unitAddress = new { locationId = Config.ReadSetting("YanziLocation") },
                subscriptionType = new { name = "default", resourceType = "SubscriptionType" }
            };
            // socket.SendMessage(JsonConvert.SerializeObject(subscribeRequest));
        }
        /// <summary>
        /// Connects to Cirrus
        /// </summary>
        /// <returns>If the connection was successfully opened</returns>
        public bool Connect()
        {
            socket = WebSocketWrapper.Create(Config.ReadSetting("YanziHost"));

            socket.OnConnect((WebSocketWrapper ws) => Console.WriteLine("Has connected"));
            socket.OnMessage(OnMessage);
            socket.OnDisconnect((WebSocketWrapper ws) => Console.WriteLine("Has disconnected"));

            socket.Connect();

            // Wait until socket is no longer trying to connect
            while (socket.State() == WebSocketState.Connecting)
            {
                Console.WriteLine("Connecting");
                Thread.Sleep(50);

            }

            if (socket.State() != WebSocketState.Open)
            {
                Console.WriteLine("Could not connect to Cirrus");
                return false;
            }
            return true;
        }

        public void Login()
        {
            var loginRequest = new LoginRequest()
            {
                username = Config.ReadSetting("YanziUser"),
                password = Config.ReadSetting("YanziPass"),
            };
            string json = JsonConvert.SerializeObject(loginRequest);
            socket.SendMessage(json);
        }

        public void Close()
        {
            // socket.Close();
        }
    }
}
