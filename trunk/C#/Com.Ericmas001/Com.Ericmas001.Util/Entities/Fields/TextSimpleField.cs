namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.Text)]
    public class TextSimpleField : SimpleField
    {
        public virtual string StringValue { get { return (string)Value; } }
    }
}
