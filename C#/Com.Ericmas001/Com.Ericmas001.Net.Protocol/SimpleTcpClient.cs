using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class SimpleTcpClient
    {
        protected TcpClient m_ServerTcp;
        protected RemoteTcpEntity m_Server;

        protected abstract RemoteTcpEntity CreateServer(TcpClient tcpClient);
        protected abstract void OnServerConnected(RemoteTcpEntity client);
        protected abstract void OnServerDisconnected(RemoteTcpEntity client);

        private readonly string m_Ip;
        private readonly int m_Port;

        public SimpleTcpClient(string ip, int port)
        {
            m_Ip = ip;
            m_Port = port;
        }

        public void Connect()
        {
            if (m_ServerTcp == null)
            {
                m_ServerTcp = new TcpClient();
                m_ServerTcp.Connect(m_Ip, m_Port);
                var task = StartHandleConnectionAsync();
                // if already faulted, re-throw any error on the calling context
                if (task.IsFaulted)
                    task.Wait();
            }
        }

        public void Send(string data)
        {
            m_Server.Send(data);
        }

        // Register and handle the connection
        private async Task StartHandleConnectionAsync()
        {
            m_Server = CreateServer(m_ServerTcp);

            // start the new connection task
            var connectionTask = m_Server.Run();

            OnServerConnected(m_Server);

            // catch all errors of HandleConnectionAsync
            try
            {
                await connectionTask;
                // we may be on another thread after "await"
            }
            catch (IOException)
            {
                OnServerDisconnected(m_Server);
            }
            finally
            {
                m_Server = null;
                m_ServerTcp = null;
            }
        }
    }
}
