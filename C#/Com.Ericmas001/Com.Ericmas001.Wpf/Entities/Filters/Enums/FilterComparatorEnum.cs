using System.ComponentModel;
using Com.Ericmas001.Util.Entities.Fields;

namespace Com.Ericmas001.Wpf.Entities.Filters.Enums
{
    public enum FilterComparatorEnum
    {
        [Description("")]
        [FieldType(FieldTypeEnum.None)]
        None,

        [FieldType(FieldTypeEnum.CheckList)]
        [Description("One of Those:")]
        TextEqual,

        [Description("Starting With")]
        [FieldType(FieldTypeEnum.Text)]
        StartsWith,

        [Description("Ending With")]
        [FieldType(FieldTypeEnum.Text)]
        EndsWith,

        [Description("Containing")]
        [FieldType(FieldTypeEnum.Text)]
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

        [FieldType(FieldTypeEnum.IntPair)]
        [Description("Between")]
        IntBetween

    }
}
