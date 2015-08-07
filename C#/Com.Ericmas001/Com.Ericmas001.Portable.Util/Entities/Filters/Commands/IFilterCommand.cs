using Com.Ericmas001.Portable.Util.Entities.Filters.Comparators;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Commands
{
    public interface IFilterCommand
    {
        string Description { get; }
        bool IsDataFiltered(IFilterComparator comparator, object comparatorValue, object value, IDataItem item);
    }
}
