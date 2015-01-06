using System.ComponentModel;
using Com.Ericmas001.Wpf.Entities.Attributes;

namespace Com.Ericmas001.Wpf.Entities.Enums
{
    public enum FilterEnum
    {
        None,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.TextEqual, FilterComparatorEnum.StartsWith, FilterComparatorEnum.Contains, FilterComparatorEnum.EndsWith)]
        [SearchType(SearchTypeEnum.Text)]
        [Description("Filter Text")]
        Text,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan, FilterComparatorEnum.IntBetween)]
        [SearchType(SearchTypeEnum.Int)]
        [Description("Filter Int")]
        Int,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [SearchType(SearchTypeEnum.Date)]
        [Description("Filter Date")]
        Date,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.IntNotEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [SearchType(SearchTypeEnum.Time)]
        [Description("Filter Time")]
        Time

    }
}
