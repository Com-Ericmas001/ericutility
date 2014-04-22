using System.Text;

namespace Com.Ericmas001.Net.Protocol.Text
{
    public class DisconnectTextCommand : AbstractTextCommand
    {
        protected override void Append<T>(StringBuilder sb, T thing)
        {
            sb.Append(thing);
        }
    }
}