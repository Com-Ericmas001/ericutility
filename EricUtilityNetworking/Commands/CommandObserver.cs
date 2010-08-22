using System;
using System.Collections.Generic;
using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class CommandObserver
    {
        public event EventHandler<StringEventArgs> CommandReceived = delegate { };

        protected abstract void receiveSomething(string line);

        public virtual void messageReceived(string line)
        {
            if (line == null)
            {
                return;
            }
            CommandReceived(this, new StringEventArgs(line));
            receiveSomething(line);
        }
    }
}
