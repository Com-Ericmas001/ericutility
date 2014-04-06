using System;
using System.Text;

namespace Com.Ericmas001.Net.Protocol.Text
{
    public abstract class AbstractTextCommand : AbstractCommand
    {
        public static char Delimitter { get { return ';'; } }

        public virtual void Encode(StringBuilder sb)
        {
        }

        protected virtual void Append<T>(StringBuilder sb, T thing)
        {
            sb.Append(thing);
            sb.Append(AbstractTextCommand.Delimitter);
        }

        public override string Encode()
        {
            StringBuilder sb = new StringBuilder();
            Append(sb, CommandName);
            Encode(sb);
            return sb.ToString();
        }
    }
}