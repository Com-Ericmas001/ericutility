using System.Net.Sockets;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class CommandTcpCommunicator1<T> : TcpCommunicator1 where T : AbstractCommandObserver, new()
    {
        protected T m_CommandObserver = new T();

        public CommandTcpCommunicator1(TcpClient socket)
            : base(socket)
        {
            InitializeCommandObserver();
        }

        protected abstract void InitializeCommandObserver();

        protected override string Receive()
        {
            var line = base.Receive();
            m_CommandObserver.MessageReceived(line);
            return line;
        }

        protected virtual void Send(AbstractCommand command)
        {
            base.Send(command.Encode());
        }
    }
}