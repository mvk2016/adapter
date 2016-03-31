using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;

/// <summary>
/// Handles the formatting of messages to and from Cirrus
/// </summary>
public class JSONConverter
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    public string MakeRequest(Dictionary<string, dynamic> data)
    {
        return jss.Serialize(data);
    }

    public T ParseMessage<T>(string response)
    {
        return jss.Deserialize<T>(response);
    }
}
