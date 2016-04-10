using System;

namespace AzureWSBridge.Requests
{
    public abstract class Request
    {
        public string messageType;
        public long timeSent;
        public Request()
        {
            this.messageType = this.GetType().Name.ToString();
            // json.net doesn't convert DateTime to a format Cirrus accepts
            timeSent = (long)DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
        }
    }
}
