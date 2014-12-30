using System.ComponentModel;
using Com.Ericmas001.AppMonitor.DataTypes.Attributes;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.AppMonitor.DataTypes.Enums
{
    public enum FilterEnum
    {
        None,

        [FilterCommand(FilterCommandEnum.Must, FilterCommandEnum.MustNot)]
        [FilterComparator(FilterComparatorEnum.TextEqual, FilterComparatorEnum.StartsWith, FilterComparatorEnum.Contains, FilterComparatorEnum.EndsWith)]
        [SearchType(SearchTypeEnum.Text)]
        [Description("Filter Text")]
        Text,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [SearchType(SearchTypeEnum.Int)]
        [Description("Filtrer Int")]
        Int,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [SearchType(SearchTypeEnum.Date)]
        [Description("Filtrer Date")]
        Date,

        [FilterCommand(FilterCommandEnum.Must)]
        [FilterComparator(FilterComparatorEnum.SmallerThan, FilterComparatorEnum.SmallerEqual, FilterComparatorEnum.IntEqual, FilterComparatorEnum.GreaterEqual, FilterComparatorEnum.GreaterThan)]
        [SearchType(SearchTypeEnum.Time)]
        [Description("Filtrer Time")]
        Time

    }
}
