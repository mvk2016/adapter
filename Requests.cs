using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;

/// <summary>
/// Handles the formatting of messages to and from Cirrus
/// </summary>
public class Requests
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    /// <summary>
    /// Converts MS time to Unix time
    /// </summary>
    /// <returns>Number of milliseconds since 1970</returns>
    private int getTime()
    {
        TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
        return (int)span.TotalSeconds;
    }

    public string MakeRequest(string requestType, Dictionary<string, string> data)
    {
        data["messageType"] = requestType;
        data["timeSent"] = getTime().ToString();
        return jss.Serialize(data);
    }

    public Dictionary<string, dynamic> ParseResponse(string response)
    {
        return jss.Deserialize<Dictionary<string, dynamic>>(response);
    }
}
