using System.Text;

namespace EricUtility.Networking.Commands
{
    public class DisconnectCommand : AbstractCommand
    {
        protected override string CommandName
        {
            get { return DisconnectCommand.COMMAND_NAME; }
        }

        public static string COMMAND_NAME = "DISCONNECT";

        public DisconnectCommand(StringTokenizer argsToken)
        {
        }

        protected override void Append<T>(StringBuilder sb, T thing)
        {
            sb.Append(thing);
        }

        public DisconnectCommand()
        {
        }
    }
}