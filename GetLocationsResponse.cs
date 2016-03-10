using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GetLocationsResponse : Response
{
    public Dictionary<string, string> responseCode;
    public List<LocationDTO> list;
}
