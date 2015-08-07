using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Portable.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Portable.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Portable.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Commands
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
        private FilterCommandEnum m_Command;

        public FilterCommandEnum Command
        {
            get { return m_Command; }
            set
            {
                m_Command = value;
                Description = EnumFactory<FilterCommandEnum>.ToString(m_Command);
            }
        }

        public abstract bool IsDataFiltered(IFilterComparator comparator, object comparatorValue, object value, IDataItem item);
    }
}
