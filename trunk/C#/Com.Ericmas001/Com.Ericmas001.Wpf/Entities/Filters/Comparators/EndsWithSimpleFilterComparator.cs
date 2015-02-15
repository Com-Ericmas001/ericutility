using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
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
