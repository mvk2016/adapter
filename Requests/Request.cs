using System;

namespace AzureWSBridge.Requests
{
    public abstract class Request
    {
        public string messageType;
        public DateTime timeSent;
        public Request()
        {
            this.messageType = this.GetType().ToString();
        }
    }
}
