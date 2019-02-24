using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    public class AppResponse
{
    public AppResponse(string message, Object data, bool success)
    {
        this.message = message;
        this.data = data;
        this.success = success;
    }

    public string message { get; set; }
    public Object data { get; set; }
    public bool success { get; set; }
}
}
