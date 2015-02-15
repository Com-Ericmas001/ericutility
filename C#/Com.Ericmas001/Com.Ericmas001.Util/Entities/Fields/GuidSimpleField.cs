using System;

namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.Guid)]
    public class GuidSimpleField : SimpleField
    {
        public Guid GuidValue { get { return (Guid)Value; } }
    }
}
