using Com.Ericmas001.Portable.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Portable.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.EndsWith)]
    public class EndsWithSimpleFilterComparator : SimpleFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value, IDataItem item)
        {
            return value.ToString().EndsWith(comparatorValue.ToString());
        }
    }
}
