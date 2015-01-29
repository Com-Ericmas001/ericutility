using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Util;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    public abstract class BasicFilterComparator : IFilterComparator
    {
        private static Dictionary<FilterComparatorEnum, BasicFilterComparator> m_AllComparators;

        public static IEnumerable<BasicFilterComparator> AllComparators(params FilterComparatorEnum[] comparators)
        {
            if (m_AllComparators == null)
            {

                m_AllComparators = new Dictionary<FilterComparatorEnum, BasicFilterComparator>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(BasicFilterComparator))))
                {
                    var c = type.GetConstructor(new Type[] { }).Invoke(new object[0]) as BasicFilterComparator;
                    foreach (FilterComparatorEnum fce in type.GetAttributeValue((FilterComparatorAttribute att) => att.Comparators))
                    {
                        c.Description = EnumFactory<FilterComparatorEnum>.ToString(fce);
                        m_AllComparators.Add(fce, c);
                    }
                }
            }
            return m_AllComparators.Where(kvp => comparators == null || !comparators.Any() || comparators.Contains(kvp.Key)).Select(kvp => kvp.Value);
        }

        public string Description { get; private set; }
        public abstract bool IsDataFiltered(object comparatorValue, object value);
    }
}
