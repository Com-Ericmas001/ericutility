using System.Text;

namespace EricUtility.Networking.Commands
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