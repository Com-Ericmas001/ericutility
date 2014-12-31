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

        [Description("Equal to")]
        [SearchType(SearchTypeEnum.List)]
        Be,

        [SearchType(SearchTypeEnum.CheckList)]
        [Description("Equal to")]
        TextEqual,

        [Description("Starting With")]
        [SearchType(SearchTypeEnum.Text)]
        StartsWith,

        [Description("Ending With")]
        [SearchType(SearchTypeEnum.Text)]
        EndsWith,

        [Description("Containing")]
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
        [Description("Between")]
        IntBetween

    }
}
