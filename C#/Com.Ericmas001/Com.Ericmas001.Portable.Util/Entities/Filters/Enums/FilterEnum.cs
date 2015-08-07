using System.ComponentModel;
using Com.Ericmas001.Portable.Util.Entities.Fields;
using Com.Ericmas001.Portable.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Portable.Util.Entities.Attributes;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Enums
{
    public enum FilterEnum
    {
        None,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.TextEqual, FilterComparatorEnum.StartsWith, FilterComparatorEnum.Contains, FilterComparatorEnum.EndsWith)]
        [FieldType(FieldTypeEnum.Text)]
        [EnumDescription("Filter Text")]
        Text,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.StartsWith, FilterComparatorEnum.Contains, FilterComparatorEnum.EndsWith)]
        [FieldType(FieldTypeEnum.Text)]
        [EnumDescription("Filter Data")]
        Blob,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan, FilterComparatorEnum.IntBetween)]
        [FieldType(FieldTypeEnum.Int)]
        [EnumDescription("Filter Int")]
        Int,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [FieldType(FieldTypeEnum.Date)]
        [EnumDescription("Filter Date")]
        Date,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [FieldType(FieldTypeEnum.Time)]
        [EnumDescription("Filter Time")]
        Time

    }
}
