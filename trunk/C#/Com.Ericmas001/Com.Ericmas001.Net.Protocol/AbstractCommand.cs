using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Text;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class AbstractCommand
    {
        public static string CommandNameField { get { return "COMMAND_NAME"; } }

        [JsonProperty(Order = -2)]
        public string CommandName { get { return (string)this.GetType().GetField(CommandNameField, (BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)).GetValue(null); } }

        public abstract string Encode();

        public AbstractCommand()
        {
            if (this.GetType().GetField(CommandNameField, (BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)) == null)
                throw new Exception("You need a public static field named '" + CommandNameField + "' that gives the name of the command!");
        }
    }
}