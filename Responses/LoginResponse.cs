using System;
using System.Collections.Generic;

namespace AzureWSBridge.Responses
{
    public class LoginResponse : Response
    {
        public string sessionId;
        public Dictionary<string, string> responseCode;

        public override void Action()
        {
            if (responseCode["name"] != "success")
            {
                Console.WriteLine("Login failed");
            }
            else
            {
                Console.WriteLine("Login success");
            }
        }
    }
}
