using Newtonsoft.Json.Linq;
using System.Text;

namespace Com.Ericmas001.Net.Commands
{
    public class DisconnectJsonCommand : AbstractJsonCommand
    {
        public static string COMMAND_NAME = "DISCONNECT";

        public DisconnectJsonCommand()
        {
        }
    }
}