using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Request
{
    public string messageType;
    public DateTime timeSent;
    public Request()
    {
        this.messageType = this.GetType().ToString();
    }
}
