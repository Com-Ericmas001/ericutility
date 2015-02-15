using System;

namespace Com.Ericmas001.Util.Entities.Fields
{
    [FieldType(FieldTypeEnum.IntPair)]
    public class IntPairSimpleField : SimpleField
    {
        public int Value1 { get { return ((Tuple<int, int>)Value).Item1; } }
        public int Value2 { get { return ((Tuple<int, int>)Value).Item2; } }

        public override string ToString()
        {
            return String.Format("{0} And {1}", Value1, Value2);
        }
    }
}
