using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Commands
{
    public abstract class SimpleFilterCommand : IFilterCommand
    {
        private static Dictionary<FilterCommandEnum, SimpleFilterCommand> m_AllCommands;

        public static IEnumerable<SimpleFilterCommand> AllCommands(params FilterCommandEnum[] commands)
        {
            if (m_AllCommands == null)
            {

                m_AllCommands = new Dictionary<FilterCommandEnum, SimpleFilterCommand>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof (SimpleFilterCommand))))
                {
                    var c = type.GetConstructor(new Type[] {}).Invoke(new object[0]) as SimpleFilterCommand;
                    foreach (FilterCommandEnum fce in type.GetAttributeValue((FilterCommandAttribute att) => att.Commands))
                    {
                        c.Description = EnumFactory<FilterCommandEnum>.ToString(fce);
                        c.Command = fce;
                        m_AllCommands.Add(fce, c);
                    }
                }
            }
            return m_AllCommands.Where(kvp => commands == null || !commands.Any() || commands.Contains(kvp.Key)).OrderBy(kvp => kvp.Value.Command).Select(kvp => kvp.Value);
        }

        public static SimpleFilterCommand GetCommand(FilterCommandEnum command)
        {
            return AllCommands(command).Single();
        }

        public string Description { get; private set; }
        public FilterCommandEnum Command { get; private set; }
        public abstract bool IsDataFiltered(IFilterComparator comparator, object comparatorValue, object value, IDataItem item);
    }
}
