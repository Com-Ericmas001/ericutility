using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Linq;

namespace Com.Ericmas001.Net.Protocol.JSON
{
    public abstract class JsonCommandObserver: AbstractCommandObserver
    {

        /// <summary>
        /// Méthode éxécutée lorsqu'on recoit une commande
        /// </summary>
        /// <param name="line">ligne de commande</param>
        /// <remarks>La méthode s'occupe de faire un RaiseEvent pour chaque evenemement EventHandler(Of CommandEventArgs(Of T)) où le nom de la classe est égal au premier token de la ligne reçu</remarks>
        protected override void ReceiveSomething(string line)
        {
            //AbstractJsonCommand command = JsonConvert.DeserializeObject<AbstractJsonCommand>(line);
            JObject jObj = JsonConvert.DeserializeObject<dynamic>(line);
            var commandName = (string)jObj["CommandName"];
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
                            var method = typeof(JsonConvert).GetMethods().First(m => m.Name == "DeserializeObject" && m.IsGenericMethod).MakeGenericMethod(new[] { commType });
                            var command = method.Invoke(null, new object[] { line });
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