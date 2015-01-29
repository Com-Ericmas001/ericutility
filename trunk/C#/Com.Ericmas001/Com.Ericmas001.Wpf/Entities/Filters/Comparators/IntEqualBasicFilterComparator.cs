using System.Collections;
using System.Linq;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.IntEqual)]
    public class IntEqualBasicFilterComparator : BasicFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value)
        {
            return value.Equals(comparatorValue);
        }
    }
}
