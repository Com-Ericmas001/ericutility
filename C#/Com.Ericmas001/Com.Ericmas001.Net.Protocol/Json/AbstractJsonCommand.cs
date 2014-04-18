using Newtonsoft.Json;
using System;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace Com.Ericmas001.Net.Protocol.JSON
{
    public abstract class AbstractJsonCommand : AbstractCommand
    {
        public override string Encode()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}