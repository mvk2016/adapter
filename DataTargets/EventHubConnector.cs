using System.Text;
using AzureWSBridge.Lib;
using Microsoft.ServiceBus.Messaging;
using System;

namespace AzureWSBridge.DataTargets
{
    /// <summary>
    /// Manages the connection to an Event Hub in Azure
    /// </summary>
    public class EventHubConnector
    {

        static EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(Config.ReadSetting("SourceHubConnectionString"), Config.ReadSetting("SourceHubName"));

        static public void SendMessage(string message)
        {
            Console.WriteLine("Sending to Event Hub");
            eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
        }
    }
}
