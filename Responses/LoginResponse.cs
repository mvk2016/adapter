﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class LoginResponse : Response
{
    public string sessionId;
    public Dictionary<string, string> responseCode;

    public override void Action()
    {
        if (responseCode["name"] != "success")
        {
            Console.WriteLine("Login failed");
        }
        else
        {
            Console.WriteLine("Login success");
        }
    }
}