using System;
using System.Collections.Generic;
using AzureWSBridge.DataTargets;
using AzureWSBridge.DataSources;

namespace AzureWSBridge.Responses
{
    class SubscribeData : Response {

        public override void Action(string message, CirrusDataSource cirrus)
        {
            base.Action(message, cirrus);
            Console.WriteLine("Sending to Event Hub {0}", DateTime.Now);
            EventHubConnector.SendMessage(message);
        }
    }
}
