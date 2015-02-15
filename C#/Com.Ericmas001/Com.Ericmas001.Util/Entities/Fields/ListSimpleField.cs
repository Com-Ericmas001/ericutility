using System;
namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.List)]
    public class ListSimpleField : SimpleField
    {
        public virtual Tuple<string, object> TupleValue { get { return (Tuple<string, object>)Value; } }
        public override string ToString()
        {
            return "{" + string.Join(", ", TupleValue.Item1) + "}";
        }
    }
}
