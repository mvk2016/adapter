using System;
using System.Reflection;
using AzureWSBridge.DataSources;

namespace AzureWSBridge.Responses
{
    public class Response
    {
        public string messageType;
        public long timeSent;

        public virtual void Action(string message, CirrusDataSource cirrus)
        {
            //Console.WriteLine(this.GetType().Name);
        }

        /// <summary>
        /// Handles
        /// </summary>
        /// <param name="message"></param>
        public void Handle(string message, CirrusDataSource cirrus)
        {
            Type type = Type.GetType(string.Format("{0}.{1}", this.GetType().Namespace, messageType));

            if(type == null)
            {
                type = typeof(Response);
            }

            Response r = Activator.CreateInstance(type) as Response;
            r.Action(message, cirrus);
        }
    }
}
