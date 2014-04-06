using Com.Ericmas001.Util;
using System.Text;

namespace Com.Ericmas001.Net.Protocol.Text
{
    public class DisconnectTextCommand : AbstractTextCommand
    {
        public static string COMMAND_NAME = "DISCONNECT";

        public DisconnectTextCommand(StringTokenizer argsToken)
        {
        }

        protected override void Append<T>(StringBuilder sb, T thing)
        {
            sb.Append(thing);
        }

        public DisconnectTextCommand()
        {
        }
    }
}