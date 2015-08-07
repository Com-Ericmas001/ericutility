using System.ComponentModel;
using Com.Ericmas001.Portable.Util.Entities.Fields;
using Com.Ericmas001.Portable.Util.Entities.Attributes;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Enums
{
    public enum FilterComparatorEnum
    {
        [EnumDescription("")]
        [FieldType(FieldTypeEnum.None)]
        None,

        [FieldType(FieldTypeEnum.CheckList)]
        [EnumDescription("One of Those:")]
        TextEqual,

        [EnumDescription("Starting With")]
        [FieldType(FieldTypeEnum.Text)]
        StartsWith,

        [EnumDescription("Ending With")]
        [FieldType(FieldTypeEnum.Text)]
        EndsWith,

        [EnumDescription("Containing")]
        [FieldType(FieldTypeEnum.Text)]
        Contains,

        [EnumDescription("=")]
        IntEqual,

        [EnumDescription("≠")]
        IntNotEqual,

        [EnumDescription("<")]
        SmallerThan,

        [EnumDescription("<=")]
        SmallerEqual,

        [EnumDescription(">=")]
        GreaterEqual,

        [EnumDescription(">")]
        GreaterThan,

        [FieldType(FieldTypeEnum.IntPair)]
        [EnumDescription("Between")]
        IntBetween

    }
}
