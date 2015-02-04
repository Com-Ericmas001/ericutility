using Com.Ericmas001.Util.Entities;
namespace Com.Ericmas001.Wpf.Entities.Filters.Comparators
{
    public interface IFilterComparator
    {
        string Description { get; }
        bool IsDataFiltered(object comparatorValue, object value, IDataItem item);
    }
}
