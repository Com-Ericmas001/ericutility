using Newtonsoft.Json;
using System;
using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractJsonCommand : AbstractCommand
    {
        public override string Encode()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}