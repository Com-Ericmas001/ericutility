using Com.Ericmas001.Collections;
using Com.Ericmas001.Util;
using System;
using System.Threading;

namespace Com.Ericmas001.Net
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
            if (m_IsConnected)
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
            m_IsConnected = true;
            new Thread(new ThreadStart(Run)).Start();
        }

        public void Incoming(string message)
        {
            m_Incoming.Enqueue(message);
        }
    }
}