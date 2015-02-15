using System;
using System.Collections.Generic;
using System.Linq;
using Com.Ericmas001.Util;
using Com.Ericmas001.Util.Entities;
using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Util.Entities.Filters;
using Com.Ericmas001.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Util.Entities.Filters.Commands;
using Com.Ericmas001.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Wpf.Entities.Filters
{
    public class SimpleFilterInCreation : BaseFilterInCreation
    {
        public FilterEnum FilterType { get; private set; }
        public SimpleFilterInCreation(string field, FilterEnum filterType, IBunchOfDataItems dataItems)
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

        protected virtual FilterInfo GenerateFilterInfo()
        {
            SimpleField sf = SimpleField.GenerateField(CurrentFieldType);
            switch (CurrentFieldType)
            {
                case FieldTypeEnum.CheckList:
                    sf.Value = AvailablesItems.Where(x => x.IsSelected).Select(x => new Tuple<string, object>(x.Name, x.Value));
                    break;
                case FieldTypeEnum.List:
                    sf.Value = CurrentValueList;
                    break;
                case FieldTypeEnum.Date:
                    sf.Value = CurrentValueDate;
                    break;
                case FieldTypeEnum.IntPair:
                    sf.Value = new Tuple<int, int>(int.Parse(CurrentValueStringPair1), int.Parse(CurrentValueStringPair2));
                    break;
                case FieldTypeEnum.Int:
                    sf.Value = int.Parse(CurrentValueString);
                    break;
                case FieldTypeEnum.Guid:
                    sf.Value = Guid.Parse(CurrentValueString);
                    break;
                default:
                    sf.Value = CurrentValueString;
                    break;
            }

            return new FilterInfo(Field, CurrentCommand, CurrentComparator, sf);
        }

        protected override BaseCompiledFilter CompileFilter()
        {
            return new SimpleCompiledFilter(GenerateFilterInfo(), FilterType);
        }
    }
}
