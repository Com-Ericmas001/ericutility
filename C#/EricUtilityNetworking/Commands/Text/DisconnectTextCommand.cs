using System.Text;

namespace EricUtility.Networking.Commands
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