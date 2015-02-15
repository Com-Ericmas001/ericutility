using Com.Ericmas001.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Util.Entities.Filters.Comparators
{
    [FilterComparator(FilterComparatorEnum.Contains)]
    public class ContainsSimpleFilterComparator : SimpleFilterComparator
    {
        public override bool IsDataFiltered(object comparatorValue, object value, IDataItem item)
        {
            return value.ToString().Contains(comparatorValue.ToString());
        }
    }
}
