using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

/// <summary>
/// Manages the connection to an Event Hub in Azure
/// </summary>
public class EventHubConnector
{
    static string eventHubName = "mvkdemoeventhub";
    static string connectionString = "Endpoint=sb://mvkdemoeventhub-ns.servicebus.windows.net/;SharedAccessKeyName=SendRule;SharedAccessKey=1Cavv5ysfeKU9aYjESuXBm7+UKX6+qOJRG0Bioi0Rug=";

    static EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

    static public void SendMessage(string message)
    {
        eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
    }
}
