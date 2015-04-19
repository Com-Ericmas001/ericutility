using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class RemoteTcpEntity
    {
        public TcpClient RemoteEntity { get; private set; }
        public StreamReader m_Input;
        public StreamWriter m_Output;
        public RemoteTcpEntity(TcpClient remoteEntity)
        {
            RemoteEntity = remoteEntity;
            m_Input = new StreamReader(remoteEntity.GetStream());
            m_Output = new StreamWriter(remoteEntity.GetStream());
            m_Output.AutoFlush = true;
        }

        // The core server task
        public Task Run()
        {
            return Task.Run(async () =>
            {
                while (true)
                    OnDataReceived(await m_Input.ReadLineAsync());
            });
        }

        protected abstract void OnDataReceived(string data);

        protected abstract void OnDataSent(string data);

        public void Send(string data)
        {
            try
            {
                Task.Run(async () => await m_Output.WriteLineAsync(data)).Wait();
                OnDataSent(data);
            }
            catch
            {
            }
        }
    }
}

