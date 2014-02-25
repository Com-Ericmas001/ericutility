using System.Net.Sockets;

namespace EricUtility.Networking.Commands
{
    public abstract class CommandTCPCommunicator<T> : TCPCommunicator where T : AbstractCommandObserver, new()
    {
        protected T m_CommandObserver = new T();

        public CommandTCPCommunicator(TcpClient socket)
            : base(socket)
        {
            InitializeCommandObserver();
        }

        protected abstract void InitializeCommandObserver();

        protected override string Receive()
        {
            string line = base.Receive();
            m_CommandObserver.messageReceived(line);
            return line;
        }

        protected virtual void Send(AbstractCommand command)
        {
            base.Send(command.Encode());
        }
    }
}