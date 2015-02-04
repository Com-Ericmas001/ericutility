using System;
using Com.Ericmas001.Util.Entities;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    public abstract class ComparableSimpleFilterComparator : SimpleFilterComparator
    {
        public abstract bool IsComparableDataFiltered(IComparable comparatorValue, IComparable value, IDataItem item);

        public override bool IsDataFiltered(object comparatorValue, object value, IDataItem item)
        {
            var comparatorValueC = comparatorValue as IComparable;
            var valueC = value as IComparable;
            if (comparatorValueC == null || valueC == null)
                throw new ArgumentException("Both parameters must implement IComparable");

            return IsComparableDataFiltered(comparatorValueC, valueC, item);
        }
    }
}
