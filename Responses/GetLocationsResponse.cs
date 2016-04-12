using System.Collections.Generic;

namespace AzureWSBridge.Responses
{
    public class GetLocationsResponse : Response
    {
        public Dictionary<string, string> responseCode;
        public List<LocationDTO> list;

        public override void Action(string message)
        {
            base.Action(message);
        }
    }
}
