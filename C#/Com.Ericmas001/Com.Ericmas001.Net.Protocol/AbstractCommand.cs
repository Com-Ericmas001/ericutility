using Newtonsoft.Json;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class AbstractCommand
    {
        [JsonProperty(Order = -2)]
        public string CommandName { get { return GetType().Name; } }

        public abstract string Encode();
    }
}