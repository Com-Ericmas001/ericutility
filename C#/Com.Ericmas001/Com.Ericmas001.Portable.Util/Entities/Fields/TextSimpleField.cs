namespace Com.Ericmas001.Portable.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.Text)]
    public class TextSimpleField : SimpleField
    {
        public virtual string StringValue { get { return (string)Value; } }
    }
}
