using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Linq;
using System.Runtime.Serialization.Formatters;

namespace Com.Ericmas001.Net.Commands
{
    public abstract class JsonCommandObserver: AbstractCommandObserver
    {

        /// <summary>
        /// Méthode éxécutée lorsqu'on recoit une commande
        /// </summary>
        /// <param name="line">ligne de commande</param>
        /// <remarks>La méthode s'occupe de faire un RaiseEvent pour chaque evenemement EventHandler(Of CommandEventArgs(Of T)) où T.COMMAND_NAME est égal au premier token de la ligne reçu</remarks>
        protected override void receiveSomething(string line)
        {
            AbstractJsonCommand command = JsonConvert.DeserializeObject<AbstractJsonCommand>(line, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple });

            foreach (EventInfo e in this.GetType().GetEvents())
            {
                if (e.EventHandlerType.Name == typeof(EventHandler<>).Name)
                {
                    Type eventType = e.EventHandlerType.GenericTypeArguments[0];
                    if (eventType.Name == typeof(CommandEventArgs<>).Name)
                    {
                        Type commType = eventType.GenericTypeArguments[0];
                        if (command.CommandName == (string)commType.GetField(AbstractCommand.CommandNameField, (BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Public)).GetValue(null))
                        {
                            MethodInfo method = typeof(JsonConvert).GetMethods().Where(m => m.Name == "DeserializeObject" && m.IsGenericMethod).First().MakeGenericMethod(new Type[] { commType });
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