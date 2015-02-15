using System.ComponentModel;
using Com.Ericmas001.Util.Entities.Fields;
using Com.Ericmas001.Util.Entities.Filters.Attributes;

namespace Com.Ericmas001.Util.Entities.Filters.Enums
{
    public enum FilterEnum
    {
        None,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.TextEqual, FilterComparatorEnum.StartsWith, FilterComparatorEnum.Contains, FilterComparatorEnum.EndsWith)]
        [FieldType(FieldTypeEnum.Text)]
        [Description("Filter Text")]
        Text,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.StartsWith, FilterComparatorEnum.Contains, FilterComparatorEnum.EndsWith)]
        [FieldType(FieldTypeEnum.Text)]
        [Description("Filter Data")]
        Blob,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan, FilterComparatorEnum.IntBetween)]
        [FieldType(FieldTypeEnum.Int)]
        [Description("Filter Int")]
        Int,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [FieldType(FieldTypeEnum.Date)]
        [Description("Filter Date")]
        Date,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [FieldType(FieldTypeEnum.Time)]
        [Description("Filter Time")]
        Time

    }
}
