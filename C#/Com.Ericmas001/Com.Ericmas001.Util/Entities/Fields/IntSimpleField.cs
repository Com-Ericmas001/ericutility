using System;

namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.Int)]
    public class IntSimpleField : SimpleField
    {
        public int IntValue { get { return (int)Value; } }
    }
}
