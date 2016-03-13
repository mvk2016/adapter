using System;
using System.Web.Script.Serialization;
using System.Collections.Generic;

/// <summary>
/// Handles the formatting of messages to and from Cirrus
/// </summary>
public class JSONConverter
{
    private JavaScriptSerializer jss = new JavaScriptSerializer();

    public string MakeRequest(Object obj)
    {
        return jss.Serialize(obj);
    }

    public T ParseResponse<T>(string response)
    {
        return jss.Deserialize<T>(response);
    }
}
