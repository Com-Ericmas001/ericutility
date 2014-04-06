using Com.Ericmas001.Util;
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace Com.Ericmas001.Net.Protocol
{
    public class TCPCommunicator
    {
        protected StreamWriter m_Output;
        protected StreamReader m_Input;
        protected TcpClient m_Socket;
        protected bool m_IsConnected;

        public TCPCommunicator(TcpClient socket)
        {
            m_Socket = socket;
            m_Output = new StreamWriter(m_Socket.GetStream());
            m_Output.AutoFlush = true;
            m_Input = new StreamReader(m_Socket.GetStream());
            m_IsConnected = false;
        }

        public TCPCommunicator()
        {
            m_IsConnected = false;
        }

        public virtual bool IsConnected
        {
            get
            {
                return m_Socket != null && m_Socket.Connected;
            }
        }

        public virtual void SetIsConnected()
        {
            if (m_Socket != null)
                m_IsConnected = true;
        }

        public virtual bool Connect(string addr, int port)
        {
            m_Socket = new TcpClient();
            try
            {
                m_Socket.Connect(addr, port);
                m_Output = new StreamWriter(m_Socket.GetStream());
                m_Output.AutoFlush = true;
                m_Input = new StreamReader(m_Socket.GetStream());
                return true;
            }
            catch (Exception e)
            {
                LogManager.Log(LogLevel.Error, "TCPCommunicator.Connect", "Error on connect: {0}", e.Message);
                m_Socket = null;
                return false;
            }
        }

        protected virtual string Receive()
        {
            try
            {
                string line = m_Input.ReadLine();
                return line;
            }
            catch
            {
                return null;
            }
        }

        protected virtual void Send(string line)
        {
            try
            {
                if (m_Output != null)
                {
                    m_Output.WriteLine(line);
                    m_Output.Flush();
                }
            }
            catch (Exception e)
            {
                OnSendCrashed(e);
            }
        }

        protected virtual void Run()
        {
            try
            {
                while (IsConnected)
                {
                    if (Receive() == null)
                        return;
                }
            }
            catch (Exception e)
            {
                OnReceiveCrashed(e);
            }
        }

        public virtual void OnReceiveCrashed(Exception e)
        {
            LogManager.Log(LogLevel.Error, "TCPCommunicator.OnReceiveCrashed", "{0}: {1}", e.GetType(), e.Message);
            LogManager.Log(LogLevel.ErrorLow, "TCPCommunicator.OnReceiveCrashed", e.StackTrace);
        }

        public virtual void OnSendCrashed(Exception e)
        {
            LogManager.Log(LogLevel.Error, "TCPCommunicator.OnSendCrashed", "{0}: {1}", e.GetType(), e.Message);
            LogManager.Log(LogLevel.ErrorLow, "TCPCommunicator.OnSendCrashed", e.StackTrace);
        }

        public void Start()
        {
            new Thread(new ThreadStart(Run)).Start();
        }

        public void Close()
        {
            if (m_Output != null)
                m_Output.Close();
            if (m_Input != null)
                m_Input.Close();
            if (m_Socket != null)
                m_Socket.Close();
            m_Output = null;
            m_Input = null;
            m_Socket = null;
        }
    }
}