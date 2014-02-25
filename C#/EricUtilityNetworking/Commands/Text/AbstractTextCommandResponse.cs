using System.Text;

namespace EricUtility.Networking.Commands
{
    public abstract class AbstractTextCommandResponse<T> : AbstractTextCommand
        where T : AbstractTextCommand
    {
        private readonly T m_Command;

        public AbstractTextCommandResponse(T command)
        {
            m_Command = command;
        }

        public T Command
        {
            get { return m_Command; }
        }

        public override void Encode(StringBuilder sb)
        {
            m_Command.Encode(sb);
        }
    }
}