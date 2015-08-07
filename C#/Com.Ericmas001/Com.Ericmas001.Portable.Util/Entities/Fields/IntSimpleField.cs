namespace Com.Ericmas001.Portable.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.Int)]
    public class IntSimpleField : SimpleField
    {
        public int IntValue { get { return (int)Value; } }
    }
}
