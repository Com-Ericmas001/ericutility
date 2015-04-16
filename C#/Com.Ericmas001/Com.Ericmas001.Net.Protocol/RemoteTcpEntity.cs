using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Net.Protocol
{
    public abstract class RemoteTcpEntity
    {
        public TcpClient RemoteEntity { get; private set; }

        public RemoteTcpEntity(TcpClient remoteEntity)
        {
            RemoteEntity = remoteEntity;
        }

        // The core server task
        public Task Run()
        {
            return Task.Run(async () =>
            {
                while (true)
                {
                    var buffer = new byte[4096];
                    var byteCount = await RemoteEntity.GetStream().ReadAsync(buffer, 0, buffer.Length);
                    if (byteCount == 0)
                        throw new IOException("Disconnected !!!");
                    OnDataReceived(Encoding.UTF8.GetString(buffer, 0, byteCount));
                }
            });
        }

        protected abstract void OnDataReceived(string data);

        protected abstract void OnDataSent(string data);

        public void Send(string data)
        {
            var serverResponseBytes = Encoding.UTF8.GetBytes(data);
            Task.Run(async () => await RemoteEntity.GetStream().WriteAsync(serverResponseBytes, 0, serverResponseBytes.Length)).Wait();
            OnDataSent(data);
        }
    }
}
