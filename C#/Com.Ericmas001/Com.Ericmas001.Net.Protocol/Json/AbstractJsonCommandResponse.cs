using Com.Ericmas001.Net.Protocol.Annotations;

namespace Com.Ericmas001.Net.Protocol.JSON
{
    public abstract class AbstractJsonCommandResponse<T> : AbstractJsonCommand
        where T : AbstractJsonCommand
    {
        private T Command { [UsedImplicitly] get; set; }

        public AbstractJsonCommandResponse()
        {
        }

        public AbstractJsonCommandResponse(T command)
        {
            Command = command;
        }
    }
}