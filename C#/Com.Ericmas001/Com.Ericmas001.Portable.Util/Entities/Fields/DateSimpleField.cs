using System;

namespace Com.Ericmas001.Portable.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.Date)]
    public class DateSimpleField : SimpleField
    {
        public DateTime DateValue { get { return (DateTime)Value; } }

        public override string ToString()
        {
            return DateValue.ToString("yyyy-MM-dd");
        }
    }
}
