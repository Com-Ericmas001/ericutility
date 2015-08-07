namespace Com.Ericmas001.Portable.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.UpperText)]
    public class UpperTextSimpleField : TextSimpleField
    {
        public override string StringValue { get { return base.StringValue.ToUpper(); } }
    }
}
