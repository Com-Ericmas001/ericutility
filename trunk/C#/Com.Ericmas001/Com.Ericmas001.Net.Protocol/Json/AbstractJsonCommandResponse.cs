using System.Text;

namespace Com.Ericmas001.Net.Protocol.JSON
{
    public abstract class AbstractJsonCommandResponse<T> : AbstractJsonCommand
        where T : AbstractJsonCommand
    {
        public T Command { get; set; }

        public AbstractJsonCommandResponse()
        {
        }

        public AbstractJsonCommandResponse(T command)
        {
            Command = command;
        }
    }
}