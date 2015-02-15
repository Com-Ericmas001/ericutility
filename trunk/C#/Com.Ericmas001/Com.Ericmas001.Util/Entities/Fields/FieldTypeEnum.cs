namespace Com.Ericmas001.Util.Entities.Fields
{
    public enum FieldTypeEnum
    {
        None,
        Text,
        UpperText,
        Int,
        IntPair,
        Date,
        Time,
        Guid,
        List,
        CheckList
    }

    public static class FieldTypeEnumExtensions
    {
        public static SimpleField GenerateField(this FieldTypeEnum type)
        {
            return SimpleField.GenerateField(type);
        }
    }
}
