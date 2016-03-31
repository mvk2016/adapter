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

    static EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(Config.ReadSetting("SourceHubConnectionString"), Config.ReadSetting("SourceHubName"));

    static public void SendMessage(string message)
    {
        eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
    }
}
