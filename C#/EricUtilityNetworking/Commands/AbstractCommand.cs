using System;
using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractCommand
    {
        public static string CommandNameField { get { return "COMMAND_NAME"; } }

        public static char Delimitter { get { return ';'; } }

        protected string CommandName { get { return (string)this.GetType().GetField(CommandNameField).GetValue(null); } }

        public virtual void Encode(StringBuilder sb)
        {
        }

        protected virtual void Append<T>(StringBuilder sb, T thing)
        {
            sb.Append(thing);
            sb.Append(AbstractCommand.Delimitter);
        }

        public string Encode()
        {
            StringBuilder sb = new StringBuilder();
            Append(sb, CommandName);
            Encode(sb);
            return sb.ToString();
        }

        public AbstractCommand()
        {
            if (this.GetType().GetField(CommandNameField) == null)
                throw new Exception("You need a public static field named '" + CommandNameField + "' that gives the name of the command!");
        }
    }
}