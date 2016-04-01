using System;

namespace AzureWSBridge.Responses
{
    public class Response
    {
        public string messageType;
        public long timeSent;

        public virtual void Action()
        {
            Console.WriteLine(messageType);
        }
    }
}
