using Com.Ericmas001.Portable.Util;
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
        /// <remarks>La méthode s'occupe de faire un RaiseEvent pour chaque evenemement EventHandler(Of CommandEventArgs(Of T)) où le nom de la classe est égal au premier token de la ligne reçu</remarks>
        protected override void ReceiveSomething(string line)
        {

            var token = new StringTokenizer(line, Delimitter);
            var commandName = token.NextToken();
            foreach (var e in GetType().GetEvents())
            {
                if (e.EventHandlerType.Name == typeof(EventHandler<>).Name)
                {
                    var eventType = e.EventHandlerType.GenericTypeArguments[0];
                    if (eventType.Name == typeof(CommandEventArgs<>).Name)
                    {
                        var commType = eventType.GenericTypeArguments[0];
                        if (commandName == commType.Name)
                        {
                            var command = Activator.CreateInstance(commType, token);
                            var commandEventArgs = Activator.CreateInstance(eventType, command);
                            var del = (MulticastDelegate)GetType().GetField(e.Name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
                            if (del != null)
                                foreach (var hdl in del.GetInvocationList())
                                    hdl.Method.Invoke(hdl.Target, new[] { this, commandEventArgs });
                        }
                    }
                }
            }
        }
    }
}