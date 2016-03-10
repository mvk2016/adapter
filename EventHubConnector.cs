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
class EventHubConnector
{
    static string eventHubName = "mvkdemoeventhub";
    static string connectionString = "Endpoint=sb://mvkdemoeventhub-ns.servicebus.windows.net/;SharedAccessKeyName=SendRule;SharedAccessKey=1Cavv5ysfeKU9aYjESuXBm7+UKX6+qOJRG0Bioi0Rug=";

    EventHubClient eventHubClient = EventHubClient.CreateFromConnectionString(connectionString, eventHubName);

    public void SendMessage(string message)
    {
        eventHubClient.Send(new EventData(Encoding.UTF8.GetBytes(message)));
    }

    public void SendingRandomMessages()
    {
        while (true)
        {
            try
            {
                var message = Guid.NewGuid().ToString();
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, message);
                SendMessage(message);
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("{0} > Exception: {1}", DateTime.Now, exception.Message);
                Console.ResetColor();
            }

            Thread.Sleep(1000);
        }
    }
}
