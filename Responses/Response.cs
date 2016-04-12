using System;
using System.Reflection;

namespace AzureWSBridge.Responses
{
    public class Response
    {
        public string messageType;
        public long timeSent;

        public virtual void Action(string message)
        {
            Console.WriteLine(this.GetType().Name);
        }

        /// <summary>
        /// Handles
        /// </summary>
        /// <param name="message"></param>
        public void Handle(string message)
        {
            Type type = Type.GetType(string.Format("{0}.{1}", this.GetType().Namespace, messageType));

            if(type == null)
            {
                type = typeof(Response);
            }

            Response r = Activator.CreateInstance(type) as Response;
            r.Action(message);
        }
    }
}
