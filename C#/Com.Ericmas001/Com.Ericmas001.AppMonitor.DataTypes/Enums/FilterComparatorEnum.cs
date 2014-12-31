using System.ComponentModel;
using Com.Ericmas001.Wpf.Entities.Attributes;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.AppMonitor.DataTypes.Enums
{
    public enum FilterComparatorEnum
    {
        [Description("")]
        [SearchType(SearchTypeEnum.None)]
        None,

        [Description("Be")]
        [SearchType(SearchTypeEnum.List)]
        Be,

        [SearchType(SearchTypeEnum.CheckList)]
        [Description("Be Equal to")]
        TextEqual,

        [Description("Starts With")]
        [SearchType(SearchTypeEnum.Text)]
        StartsWith,

        [Description("Ends With")]
        [SearchType(SearchTypeEnum.Text)]
        EndsWith,

        [Description("Contain")]
        [SearchType(SearchTypeEnum.Text)]
        Contains,

        [Description("=")]
        IntEqual,

        [Description("≠")]
        IntNotEqual,

        [Description("<")]
        SmallerThan,

        [Description("<=")]
        SmallerEqual,

        [Description(">=")]
        GreaterEqual,

        [Description(">")]
        GreaterThan,

        [SearchType(SearchTypeEnum.IntPair)]
        [Description("between")]
        IntBetween

    }
}
