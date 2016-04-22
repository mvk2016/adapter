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
            Create();
        }
        private void Create()
        {
            if (!Connect()) return;

            Login();

            Subscribe();
        }

        /// <summary>
        /// The function to be called for each message received on listening websocket
        /// </summary>
        /// <param name="message">The message received</param>
        /// <param name="ws">The websocket on which the message was received</param>
        void OnMessage(string message, WebSocketWrapper ws)
        {
            Console.WriteLine(message);
            if (message == null || message == "")
                return;
            
            try
            {
                Response response = JsonConvert.DeserializeObject<Response>(message);
                response.Handle(message);
            }
            catch (WebSocketException e)
            {
                Console.WriteLine("Exception in websocket:");
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                Console.WriteLine(message);
                Console.WriteLine("{0}: {1}", e.GetType(), e.Message);
                ws.Close();
            }
        }

        /// <summary>
        /// Subscribes to config locations
        /// </summary>
        public void Subscribe()
        {
            var subscribeRequest = new SubscribeRequest()
            {
                unitAddress = new { locationId = Config.ReadSetting("YanziLocation") },
                subscriptionType = new { name = "default", resourceType = "SubscriptionType" }
            };
            socket.SendMessage(JsonConvert.SerializeObject(subscribeRequest));
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
            socket.OnDisconnect((WebSocketWrapper ws) => Create());

            try
            {
                socket.Connect();
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect to Cirrus");
                //Console.WriteLine(e);
                return false;
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
    }
}
