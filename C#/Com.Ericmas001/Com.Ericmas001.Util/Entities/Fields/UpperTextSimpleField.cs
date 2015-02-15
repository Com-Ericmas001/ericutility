namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.UpperText)]
    public class UpperTextSimpleField : TextSimpleField
    {
        public override string StringValue { get { return base.StringValue.ToUpper(); } }
    }
}
