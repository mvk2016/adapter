using System.Text;
using AzureWSBridge.Lib;
using Microsoft.ServiceBus.Messaging;

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
            eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
        }
    }
}
