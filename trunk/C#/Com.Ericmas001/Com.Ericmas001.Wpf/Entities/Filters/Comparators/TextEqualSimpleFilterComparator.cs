﻿using System.Collections;
using System.Linq;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.TextEqual)]
    public class TextEqualSimpleFilterComparator : SimpleFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value, IDataItem item)
        {
            var enumerable = comparatorValue as IEnumerable;
            if (enumerable != null)
                return enumerable.Cast<object>().Contains(value);

            return value.Equals(comparatorValue);
        }
    }
}
