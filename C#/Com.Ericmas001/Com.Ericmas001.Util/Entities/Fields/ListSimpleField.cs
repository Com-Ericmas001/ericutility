using System;
namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.List)]
    public class ListSimpleField : SimpleField
    {
        public virtual FieldListItem TupleValue { get { return (FieldListItem)Value; } }
        public override string ToString()
        {
            return "{" + string.Join(", ", TupleValue.Name) + "}";
        }
    }
}
