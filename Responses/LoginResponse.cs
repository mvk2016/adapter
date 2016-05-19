using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using AzureWSBridge.DataSources;

namespace AzureWSBridge.Responses
{
    public class LoginResponse : Response
    {
        public string sessionId;
        public Dictionary<string, string> responseCode;

        public override void Action(string message, CirrusDataSource cirrus)
        {
            base.Action(message, cirrus);
            LoginResponse r = JsonConvert.DeserializeObject<LoginResponse>(message);
            
            if (r.responseCode["name"] != "success")
            {
                throw new Exception("Login failed");
            }
            else
            {
                Console.WriteLine("Login success");
            }
            
        }
    }
}
