using System.Net.Sockets;
using System.IO;
using System.Threading;
using System;
using EricUtility.Collections;

namespace EricUtility.Networking
{
    public class QueueCommunicator
    {
        protected bool m_IsConnected;
        protected BlockingQueue<String> m_Incoming = new BlockingQueue<String>();
        protected BlockingQueue<String> m_Outcoming = new BlockingQueue<String>();
        public event EventHandler<KeyEventArgs<string>> ReceivedSomething = delegate { };
        public event EventHandler<KeyEventArgs<string>> SendedSomething = delegate { };
        public QueueCommunicator()
        {
            m_IsConnected = false;
        }

        public virtual bool IsConnected
        {
            get { return m_IsConnected; }
            set { m_IsConnected = value; } 
        }

        protected string Receive()
        {
            return m_Incoming.Dequeue();
        }

        public void Send(string line)
        {
            m_Outcoming.Enqueue(line);
        }
        protected virtual void Run()
        {
            while (IsConnected)
            {
                Receive();
            }
        }
        public void Start()
        {
            new Thread(new ThreadStart(Run)).Start();
        }

    }
}
