using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class SimpleTcpServer
    {
        private readonly object m_LockTasks = new Object(); // sync lock 
        private readonly List<Task> m_ConnectionTasks = new List<Task>(); // pending connections

        private readonly object m_LockClients = new Object(); // sync lock 
        private readonly List<RemoteTcpEntity> m_Clients = new List<RemoteTcpEntity>(); // pending connections

        private int m_Port;
        public SimpleTcpServer(int port)
        {
            m_Port = port;
        }

        protected abstract RemoteTcpEntity CreateClient(TcpClient tcpClient);
        protected abstract void OnClientConnected(RemoteTcpEntity client);
        protected abstract void OnClientDisconnected(RemoteTcpEntity client);

        // The core server task
        public Task Run()
        {
            return Task.Run(async () =>
            {
                var tcpListener = TcpListener.Create(m_Port);
                tcpListener.Start();
                while (true)
                {
                    var tcpClient = await tcpListener.AcceptTcpClientAsync();
                    var task = StartHandleConnectionAsync(tcpClient);
                    // if already faulted, re-throw any error on the calling context
                    if (task.IsFaulted)
                        task.Wait();
                }
            });
        }

        // Register and handle the connection
        private async Task StartHandleConnectionAsync(TcpClient tcpClient)
        {
            var client = CreateClient(tcpClient);

            // add it to the list of client 
            lock (m_LockClients)
                m_Clients.Add(client);

            // start the new connection task
            var connectionTask = client.Run();

            // add it to the list of pending task 
            lock (m_LockTasks)
                m_ConnectionTasks.Add(connectionTask);

            OnClientConnected(client);

            // catch all errors of HandleConnectionAsync
            try
            {
                await connectionTask;
                // we may be on another thread after "await"
            }
            catch (IOException)
            {
                OnClientDisconnected(client);
            }
            finally
            {
                // remove pending task
                lock (m_LockTasks)
                    m_ConnectionTasks.Remove(connectionTask);

                // remove client
                lock (m_LockClients)
                    m_Clients.Add(client);
            }
        }
    }
}
