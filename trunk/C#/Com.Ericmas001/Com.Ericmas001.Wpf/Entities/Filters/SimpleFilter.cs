using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Commands;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public class SimpleFilter : BaseFilter
    {

        public SimpleFilter(string field, FilterEnum filterType, IBunchOfDataItems dataItems)
            :base(field,filterType,dataItems)
        {
        }

        protected override SearchTypeEnum GenerateSearchType()
        {
            //TODO: Unbound from Basic !!!
            var comp = CurrentComparator as SimpleFilterComparator;
            if (comp != null)
            {
                var compAtt = comp.SearchTypeOverrideAttribute;
                if (compAtt != null)
                    return compAtt.SearchType;
            }

            var filterAtt = EnumFactory<FilterEnum>.GetAttribute<SearchTypeAttribute>(FilterType);
            if (filterAtt != null)
                return filterAtt.SearchType;

            return SearchTypeEnum.None;
        }

        protected override IEnumerable<IFilterCommand> GetAllCommands()
        {
            return SimpleFilterCommand.AllCommands(EnumFactory<FilterEnum>.GetAttribute<FilterCommandAttribute>(FilterType).Commands.ToArray());
        }

        protected override IEnumerable<IFilterComparator> GetAllComparators()
        {
            return SimpleFilterComparator.AllComparators(EnumFactory<FilterEnum>.GetAttribute<FilterComparatorAttribute>(FilterType).Comparators.ToArray());
        }

        protected override bool CheckIfIsSurvivingTheFilter(string value)
        {
            switch (FilterType)
            {
                //TODO: Unbound from Basic !!!
                case FilterEnum.Text:
                case FilterEnum.Blob:
                    if (CurrentComparator is TextEqualSimpleFilterComparator)
                        return CurrentCommand.IsDataFiltered(CurrentComparator, AvailablesItems.Where(x => x.IsSelected).Select(x => (string)x.Value), value);
                    return CurrentCommand.IsDataFiltered(CurrentComparator, CurrentValueString, value);
                case FilterEnum.Int:
                    if (CurrentComparator is IntBetweenSimpleFilterComparator)
                        return CurrentCommand.IsDataFiltered(CurrentComparator, new Tuple<int, int>(int.Parse(CurrentValueStringPair1), int.Parse(CurrentValueStringPair2)), int.Parse(value));
                    return CurrentCommand.IsDataFiltered(CurrentComparator, int.Parse(CurrentValueString), int.Parse(value));
                case FilterEnum.Date:
                case FilterEnum.Time:
                    return CurrentCommand.IsDataFiltered(CurrentComparator, FilterType == FilterEnum.Date ? CurrentValueDate.ToString("yyyy-MM-dd") : CurrentValueString, value);
            }

            return true;
        }
    }
}
