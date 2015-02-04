using System;
using System.Collections;
using System.Linq;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.IntBetween)]
    public class IntBetweenSimpleFilterComparator : SimpleFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value, IDataItem item)
        {
            var comparatorValues = comparatorValue as Tuple<int, int>;
            if (comparatorValues == null)
                throw new ArgumentException("comparatorValue must be a Tuple<int,int>");
            var valueC = value as int?;
            if (valueC == null || !valueC.HasValue)
                throw new ArgumentException("value must be an int");
            return valueC.Value >= comparatorValues.Item1 && valueC.Value <= comparatorValues.Item2;
        }
    }
}
