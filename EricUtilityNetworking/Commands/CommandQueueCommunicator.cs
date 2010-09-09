using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;

namespace EricUtility.Networking.Commands
{
    public abstract class CommandQueueCommunicator<T> : QueueCommunicator where T : CommandObserver, new()
    {
        protected T m_CommandObserver = new T();

        public CommandQueueCommunicator(TcpClient socket)
        {
            InitializeCommandObserver();
            base.ReceivedSomething += new EventHandler<KeyEventArgs<string>>(CommandQueueCommunicator_ReceivedSomething);
        }

        void CommandQueueCommunicator_ReceivedSomething(object sender, KeyEventArgs<string> e)
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
