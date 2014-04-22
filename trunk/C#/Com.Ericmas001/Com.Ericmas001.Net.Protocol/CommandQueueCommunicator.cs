using Com.Ericmas001.Util;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class CommandQueueCommunicator<T> : QueueCommunicator where T : AbstractCommandObserver, new()
    {
        protected T m_CommandObserver = new T();

        protected CommandQueueCommunicator()
        {
            InitializeCommandObserver();
            ReceivedSomething += CommandQueueCommunicator_ReceivedSomething;
        }

        private void CommandQueueCommunicator_ReceivedSomething(object sender, KeyEventArgs<string> e)
        {
            m_CommandObserver.MessageReceived(e.Key);
        }

        protected abstract void InitializeCommandObserver();

        public virtual void Send(AbstractCommand command)
        {
            Send(command.Encode());
        }
    }
}