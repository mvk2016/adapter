using System.Collections.Generic;
using AzureWSBridge.DataSources;
using Newtonsoft.Json;
using AzureWSBridge.Lib;

namespace AzureWSBridge.Responses
{
    public class GetLocationsResponse : Response
    {
        public Dictionary<string, string> responseCode;
        public List<LocationDTO> list;

        public override void Action(string message, CirrusDataSource cirrus)
        {
            base.Action(message, cirrus);
            GetLocationsResponse locationsResponse = JsonConvert.DeserializeObject<GetLocationsResponse>(message);
            if(locationsResponse.list.Count > 0 && locationsResponse.list[0].locationAddress["locationId"] == Config.ReadSetting("YanziLocation"))
            {
                cirrus.Subscribe();
            }
            //cirrus.Subscribe();
        }
    }
}
