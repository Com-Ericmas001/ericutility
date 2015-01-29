using System.Collections;
using System.Linq;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.Contains)]
    public class ContainsBasicFilterComparator : BasicFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value)
        {
            return value.ToString().Contains(comparatorValue.ToString());
        }
    }
}
