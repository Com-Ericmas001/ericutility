using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractJsonCommandResponse<T> : AbstractJsonCommand
        where T : AbstractJsonCommand
    {
        private readonly T m_Command;

        public AbstractJsonCommandResponse(T command)
        {
            m_Command = command;
        }

        public T Command
        {
            get { return m_Command; }
        }
    }
}