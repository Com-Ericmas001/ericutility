using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Util.Entities.Filters.Commands;
using Com.Ericmas001.Util.Entities.Filters.Comparators;

namespace Com.Ericmas001.Util.Entities.Filters
{
    public class FilterInfo
    {
        public FilterInfo(string fieldToFilter, IFilterCommand command, IFilterComparator comparator, IField filterValue)
        {
            FieldToFilter = fieldToFilter;
            Command = command;
            Comparator = comparator;
            FilterValue = filterValue;
        }

        public string FieldToFilter { get; set; }
        public IFilterCommand Command { get; set; }
        public IFilterComparator Comparator { get; set; }

        public IField FilterValue { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", FieldToFilter, Command.Description, Comparator.Description, FilterValue);
        }
    }
}
