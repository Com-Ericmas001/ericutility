using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Wpf.Entities.Filters.Attributes;
using Com.Ericmas001.Wpf.Entities.Filters.Commands;
using Com.Ericmas001.Wpf.Entities.Filters.Comparators;
using Com.Ericmas001.Wpf.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public class SimpleFilter : BaseFilter
    {
        public FilterEnum FilterType { get; private set; }
        public SimpleFilter(string field, FilterEnum filterType, IBunchOfDataItems dataItems)
            :base(field,dataItems)
        {
            FilterType = filterType;
        }

        protected override FieldTypeEnum GenerateFieldType()
        {
            var comp = CurrentComparator as SimpleFilterComparator;
            if (comp != null)
            {
                var compAtt = comp.FieldTypeOverrideAttribute;
                if (compAtt != null)
                    return compAtt.FieldType;
            }

            var filterAtt = EnumFactory<FilterEnum>.GetAttribute<FieldTypeAttribute>(FilterType);
            if (filterAtt != null)
                return filterAtt.FieldType;

            return FieldTypeEnum.None;
        }

        protected override IEnumerable<IFilterCommand> GetAllCommands()
        {
            return SimpleFilterCommand.AllCommands(EnumFactory<FilterEnum>.GetAttribute<FilterCommandAttribute>(FilterType).Commands.ToArray());
        }

        protected override IEnumerable<IFilterComparator> GetAllComparators()
        {
            return SimpleFilterComparator.AllComparators(EnumFactory<FilterEnum>.GetAttribute<FilterComparatorAttribute>(FilterType).Comparators.ToArray());
        }

        protected override bool CheckIfIsSurvivingTheFilter(string value, IDataItem item)
        {
            switch (FilterType)
            {
                case FilterEnum.Text:
                case FilterEnum.Blob:
                    if (CurrentComparator is TextEqualSimpleFilterComparator)
                        return CurrentCommand.IsDataFiltered(CurrentComparator, AvailablesItems.Where(x => x.IsSelected).Select(x => (string)x.Value), value, item);
                    return CurrentCommand.IsDataFiltered(CurrentComparator, CurrentValueString, value, item);
                case FilterEnum.Int:
                    if (CurrentComparator is IntBetweenSimpleFilterComparator)
                        return CurrentCommand.IsDataFiltered(CurrentComparator, new Tuple<int, int>(int.Parse(CurrentValueStringPair1), int.Parse(CurrentValueStringPair2)), int.Parse(value), item);
                    return CurrentCommand.IsDataFiltered(CurrentComparator, int.Parse(CurrentValueString), int.Parse(value), item);
                case FilterEnum.Date:
                case FilterEnum.Time:
                    return CurrentCommand.IsDataFiltered(CurrentComparator, FilterType == FilterEnum.Date ? CurrentValueDate.ToString("yyyy-MM-dd") : CurrentValueString, value, item);
            }

            return true;
        }

        protected override CheckListItem[] GenerateAvailablesItems()
        {
            if (CurrentFieldType != FieldTypeEnum.List && CurrentFieldType != FieldTypeEnum.CheckList)
                return new CheckListItem[0];

            return DataItems.ObtainAllValues(Field).Select(x => new CheckListItem(RenameEmptyItem(x), x)).ToArray();
        }

        private string RenameEmptyItem(string it)
        {
            if (String.IsNullOrEmpty(it))
                return "{Empty}";
            return it;
        }
    }
}
