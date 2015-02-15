using System;
using System.Linq;
using System.Collections.Generic;

namespace Com.Ericmas001.Util.Entities.Fields
{

    [FieldType(FieldTypeEnum.CheckList)]
    public class CheckListSimpleField : SimpleField
    {
        public virtual IEnumerable<Tuple<string, object>> Values { get { return (IEnumerable<Tuple<string, object>>)Value; } }
        public override string ToString()
        {
            return "{" + string.Join(", ", Values.Select(x => x.Item1)) + "}";
        }
    }
}
