using Newtonsoft.Json;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class AbstractCommand
    {
        /// <summary>
        /// Always contains '{CommandName}' to distinguish the command from others."
        /// </summary>
        [JsonProperty(Order = -2)]
        [ExampleValue("{CommandName}")]
        public string CommandName { get { return GetType().Name; } }

        public abstract string Encode();
    }
}