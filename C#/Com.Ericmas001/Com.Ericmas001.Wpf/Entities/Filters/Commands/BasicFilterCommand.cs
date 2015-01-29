using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Commands
{
    public abstract class BasicFilterCommand : IFilterCommand
    {
        private static Dictionary<FilterCommandEnum, BasicFilterCommand> m_AllCommands;

        public static IEnumerable<BasicFilterCommand> AllCommands(params FilterCommandEnum[] commands)
        {
            if (m_AllCommands == null)
            {

                m_AllCommands = new Dictionary<FilterCommandEnum, BasicFilterCommand>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof (BasicFilterCommand))))
                {
                    var c = type.GetConstructor(new Type[] {}).Invoke(new object[0]) as BasicFilterCommand;
                    foreach (FilterCommandEnum fce in type.GetAttributeValue((FilterCommandAttribute att) => att.Commands))
                    {
                        c.Description = EnumFactory<FilterCommandEnum>.ToString(fce);
                        m_AllCommands.Add(fce, c);
                    }
                }
            }
            return m_AllCommands.Where(kvp => commands == null || !commands.Any() || commands.Contains(kvp.Key)).Select(kvp => kvp.Value);
        }

        public string Description { get; private set; }
        public abstract bool IsDataFiltered(IFilterComparator comparator, object comparatorValue, object value);
    }
}
