using System;
using System.Reflection;

namespace EricUtility.Networking.Commands
{
    public abstract class CommandObserver
    {
        public event EventHandler<StringEventArgs> CommandReceived = delegate { };

        protected abstract char Delimitter { get; }

        /// <summary>
        /// Méthode éxécutée lorsqu'on recoit une commande
        /// </summary>
        /// <param name="line">ligne de commande</param>
        /// <remarks>La méthode s'occupe de faire un RaiseEvent pour chaque evenemement EventHandler(Of CommandEventArgs(Of T)) où T.COMMAND_NAME est égal au premier token de la ligne reçu</remarks>
        protected virtual void receiveSomething(string line)
        {

            StringTokenizer token = new StringTokenizer(line, Delimitter);
            string commandName = token.NextToken();
            foreach (EventInfo e in this.GetType().GetEvents())
            {
                if (e.EventHandlerType.Name == typeof(EventHandler<>).Name)
                {
                    Type eventType = e.EventHandlerType.GenericTypeArguments[0];
                    if (eventType.Name == typeof(CommandEventArgs<>).Name)
                    {
                        Type commType = eventType.GenericTypeArguments[0];
                        if (commandName == (string)commType.GetField(AbstractCommand.CommandNameField).GetValue(null))
                        {
                            object command = Activator.CreateInstance(commType, token);
                            object commandEventArgs = Activator.CreateInstance(eventType, command);
                            MulticastDelegate del = (MulticastDelegate)this.GetType().GetField(e.Name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
                            if (del != null)
                                foreach (Delegate hdl in del.GetInvocationList())
                                    hdl.Method.Invoke(hdl.Target, new object[] { this, commandEventArgs });
                        }
                    }
                }
            }
        }

        public virtual void messageReceived(string line)
        {
            if (line == null)
            {
                return;
            }
            CommandReceived(this, new StringEventArgs(line));
            receiveSomething(line);
        }
    }
}