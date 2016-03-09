using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JSONMessage
{
    public abstract class Request
    {
        public string messageType;
        public DateTime timeSent;
        public Request()
        {
            // How would you otherwise remove "JSONMessage+" ? 
            this.messageType = this.GetType().ToString().Substring(12);
        }
    }
    public class LoginRequest : Request
    {
        public string username, password;
    }
    public class SubscribeRequest : Request
    {
        public Dictionary<string, string> unitAddress, subscriptionType;
    }



    public class Response
    {
        public string messageType;
        public long timeSent;
    }
    public class LoginResponse : Response
    {
        public string sessionId;
        public Dictionary<string, string> responseCode;
    }
    public class GetLocationsResponse : Response
    {
        public Dictionary<string, string> responseCode;
        public List<LocationDTO> list;
    }
    public class LocationDTO
    {
        public string resourceType, accountId, name, gwdid;
        public long timeCreated, timeModified;
        public Dictionary<string, dynamic> locationAddress;
    }
}
