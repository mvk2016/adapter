using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LocationDTO
{
    public string resourceType, accountId, name, gwdid;
    public long timeCreated, timeModified;
    public Dictionary<string, dynamic> locationAddress;
}
