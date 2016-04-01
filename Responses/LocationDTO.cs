using System.Collections.Generic;

namespace AzureWSBridge.Responses
{
    public class LocationDTO
    {
        public string resourceType, accountId, name, gwdid;
        public long timeCreated, timeModified;
        public Dictionary<string, dynamic> locationAddress;
    }
}
