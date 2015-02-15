namespace Com.Ericmas001.Util.Entities.Filters.Comparators
{
    public interface IFilterComparator
    {
        string Description { get; }
        bool IsDataFiltered(object comparatorValue, object value, IDataItem item);
    }
}
