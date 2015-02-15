using Com.Ericmas001.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Util.Entities.Filters.Commands
{
    [FilterCommand(FilterCommandEnum.Must)]
    public class MustSimpleFilterCommand : SimpleFilterCommand
    {
        public override bool IsDataFiltered(IFilterComparator comparator, object comparatorValue, object value, IDataItem item)
        {
            return comparator.IsDataFiltered(comparatorValue,value,item);
        }
    }
}
