namespace Com.Ericmas001.Portable.Util.Entities.Fields
{
    public class BaseField : IField
    {
        public object Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
