using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractCommand
    {
        public static char Delimitter { get { return ';'; } }
        protected abstract string CommandName { get; }

        public virtual void Encode(StringBuilder sb) { }


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
    }
}
