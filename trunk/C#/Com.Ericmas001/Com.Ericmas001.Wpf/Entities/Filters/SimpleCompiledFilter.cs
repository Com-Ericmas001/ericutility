using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Filters;
using Com.Ericmas001.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public class SimpleCompiledFilter : BaseCompiledFilter
    {
        public FilterEnum FilterType { get; private set; }
        public SimpleCompiledFilter(FilterInfo info, FilterEnum filterType)
            :base(info)
        {
            FilterType = filterType;
        }

        protected override bool CheckIfIsSurvivingTheFilter(string value, IDataItem item)
        {
            switch (FilterType)
            {
                case FilterEnum.Text:
                case FilterEnum.Blob:
                    if (Info.Comparator is TextEqualSimpleFilterComparator)
                        return Info.Command.IsDataFiltered(Info.Comparator, ((IEnumerable<Tuple<string,object>>) Info.FilterValue.Value).Select(x => (string)x.Item2), value, item);
                    return Info.Command.IsDataFiltered(Info.Comparator, Info.FilterValue.Value, value, item);
                case FilterEnum.Int:
                    return Info.Command.IsDataFiltered(Info.Comparator, Info.FilterValue.Value, int.Parse(value), item);
                case FilterEnum.Date:
                case FilterEnum.Time:
                    return Info.Command.IsDataFiltered(Info.Comparator, FilterType == FilterEnum.Date ? ((DateTime)Info.FilterValue.Value).ToString("yyyy-MM-dd") : Info.FilterValue.Value, value, item);
            }

            return true;
        }
    }
}
