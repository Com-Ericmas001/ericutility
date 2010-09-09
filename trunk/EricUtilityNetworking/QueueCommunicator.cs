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
            string line = m_Incoming.Dequeue();
            ReceivedSomething(this, new KeyEventArgs<string>(line));
            return line;
        }

        public void Send(string line)
        {
            SendedSomething(this, new KeyEventArgs<string>(line));
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
        public void Incoming(string message)
        {
            m_Incoming.Enqueue(message);
        }
    }
}
