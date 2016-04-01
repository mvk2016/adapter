using System;
using System.Collections.Generic;
using AzureWSBridge.DataTargets;

namespace AzureWSBridge.Responses
{
    class SubscribeData : Response
    {
        //public string Message { get; set; }
        // public static JSONConverter json = new JSONConverter();

        public List<Object> list;
        public override void Action()
        {
            base.Action();
            // string req = json.MakeRequest(list[0]);
            //Console.WriteLine(req);
            // EventHubConnector.SendMessage(req);
        }
    }
}
