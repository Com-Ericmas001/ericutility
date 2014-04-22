using System;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class AbstractCommandObserver
    {
        public event EventHandler<StringEventArgs> CommandReceived = delegate { };

        protected abstract void ReceiveSomething(string line);

        public virtual void MessageReceived(string line)
        {
            if (line == null)
            {
                return;
            }
            CommandReceived(this, new StringEventArgs(line));
            ReceiveSomething(line);
        }
    }
}