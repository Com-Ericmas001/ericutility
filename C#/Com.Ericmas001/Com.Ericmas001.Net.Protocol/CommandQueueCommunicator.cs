using Com.Ericmas001.Util;
using System;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class CommandQueueCommunicator<T> : QueueCommunicator where T : AbstractCommandObserver, new()
    {
        protected T m_CommandObserver = new T();

        public CommandQueueCommunicator()
        {
            InitializeCommandObserver();
            base.ReceivedSomething += new EventHandler<KeyEventArgs<string>>(CommandQueueCommunicator_ReceivedSomething);
        }

        private void CommandQueueCommunicator_ReceivedSomething(object sender, KeyEventArgs<string> e)
        {
            m_CommandObserver.messageReceived(e.Key);
        }

        protected abstract void InitializeCommandObserver();

        public virtual void Send(AbstractCommand command)
        {
            base.Send(command.Encode());
        }
    }
}