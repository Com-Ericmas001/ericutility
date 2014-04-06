using Com.Ericmas001.Util;
using System;
using System.Reflection;

namespace Com.Ericmas001.Net.Protocol.Text
{
    public abstract class TextCommandObserver: AbstractCommandObserver
    {

        protected abstract char Delimitter { get; }

        /// <summary>
        /// Méthode éxécutée lorsqu'on recoit une commande
        /// </summary>
        /// <param name="line">ligne de commande</param>
        /// <remarks>La méthode s'occupe de faire un RaiseEvent pour chaque evenemement EventHandler(Of CommandEventArgs(Of T)) où T.COMMAND_NAME est égal au premier token de la ligne reçu</remarks>
        protected override void receiveSomething(string line)
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
                        if (commandName == (string)commType.GetField(AbstractCommand.CommandNameField, (BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)).GetValue(null))
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
    }
}