using System;
using System.Collections;
using System.Linq;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.IntBetween)]
    public class IntBetweenBasicFilterComparator : BasicFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value)
        {
            var comparatorValues = comparatorValue as Tuple<IComparable, IComparable>;
            if (comparatorValues == null)
                throw new ArgumentException("comparatorValue must be a Tuple<IComparable,IComparable>");
            var valueC = value as IComparable;
            if (valueC == null)
                throw new ArgumentException("value must implement IComparable");
            return valueC.CompareTo(comparatorValues.Item1) >= 0 && valueC.CompareTo(comparatorValues.Item2) <= 0;
        }
    }
}
