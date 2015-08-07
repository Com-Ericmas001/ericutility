using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Portable.Util.Entities.Fields;
using Com.Ericmas001.Portable.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Portable.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Comparators
{
    public abstract class SimpleFilterComparator : IFilterComparator
    {
        private static Dictionary<FilterComparatorEnum, SimpleFilterComparator> m_AllComparators;

        public static IEnumerable<SimpleFilterComparator> AllComparators(params FilterComparatorEnum[] comparators)
        {
            if (m_AllComparators == null)
            {

                m_AllComparators = new Dictionary<FilterComparatorEnum, SimpleFilterComparator>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(SimpleFilterComparator))))
                {
                    var c = type.GetConstructor(new Type[] { }).Invoke(new object[0]) as SimpleFilterComparator;
                    foreach (FilterComparatorEnum fce in type.GetAttributeValue((FilterComparatorAttribute att) => att.Comparators))
                    {
                        c.Comparator = fce;
                        m_AllComparators.Add(fce, c);
                    }
                }
            }
            return m_AllComparators.Where(kvp => comparators == null || !comparators.Any() || comparators.Contains(kvp.Key)).OrderBy(kvp => kvp.Value.Comparator).Select(kvp => kvp.Value);
        }

        public static SimpleFilterComparator GetComparator(FilterComparatorEnum comparator)
        {
            return AllComparators(comparator).Single();
        }
        public FieldTypeAttribute FieldTypeOverrideAttribute { get; private set; }
        public string Description { get; private set; }

        private FilterComparatorEnum m_Comparator;

        public FilterComparatorEnum Comparator
        {
            get { return m_Comparator; }
            set
            {
                m_Comparator = value;
                Description = EnumFactory<FilterComparatorEnum>.ToString(m_Comparator);
                FieldTypeOverrideAttribute = EnumFactory<FilterComparatorEnum>.GetAttribute<FieldTypeAttribute>(m_Comparator);
            }
        }
        public abstract bool IsDataFiltered(object comparatorValue, object value, IDataItem item);
    }
}
