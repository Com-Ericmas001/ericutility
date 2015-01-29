using System;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    public abstract class BasicComparableFilterComparator : BasicFilterComparator
    {
        public abstract bool IsComparableDataFiltered(IComparable comparatorValue, IComparable value);

        public override bool IsDataFiltered(object comparatorValue, object value)
        {
            var comparatorValueC = comparatorValue as IComparable;
            var valueC = value as IComparable;
            if (comparatorValueC == null || valueC == null)
                throw new ArgumentException("Both parameters must implement IComparable");

            return IsComparableDataFiltered(comparatorValueC, valueC);
        }
    }
}
