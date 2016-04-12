using System;
using System.Collections.Generic;
using AzureWSBridge.DataTargets;

namespace AzureWSBridge.Responses
{
    class SubscribeData : Response {

        public override void Action(string message)
        {
            base.Action(message);
            EventHubConnector.SendMessage(message);
        }
    }
}
