namespace Com.Ericmas001.Portable.Util.Entities.Filters.Comparators
{
    public interface IFilterComparator
    {
        string Description { get; }
        bool IsDataFiltered(object comparatorValue, object value, IDataItem item);
    }
}
