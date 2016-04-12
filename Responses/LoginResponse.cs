using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzureWSBridge.Responses
{
    public class LoginResponse : Response
    {
        public string sessionId;
        public Dictionary<string, string> responseCode;

        public override void Action(string message)
        {
            base.Action(message);
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
