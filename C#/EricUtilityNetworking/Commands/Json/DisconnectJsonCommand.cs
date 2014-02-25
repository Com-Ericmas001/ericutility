using Newtonsoft.Json.Linq;
using System.Text;

namespace EricUtility.Networking.Commands
{
    public class DisconnectJsonCommand : AbstractTextCommand
    {
        public static string COMMAND_NAME = "DISCONNECT";

        public DisconnectJsonCommand(JObject obj)
        {
        }

        public DisconnectJsonCommand()
        {
        }
    }
}