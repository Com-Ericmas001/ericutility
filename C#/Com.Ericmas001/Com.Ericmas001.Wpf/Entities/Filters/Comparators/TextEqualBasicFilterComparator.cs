using System.Collections;
using System.Linq;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.TextEqual)]
    public class TextEqualBasicFilterComparator : BasicFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value)
        {
            var enumerable = comparatorValue as IEnumerable;
            if (enumerable != null)
                return enumerable.Cast<object>().Contains(value);

            return value.Equals(comparatorValue);
        }
    }
}
