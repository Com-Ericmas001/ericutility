using System.IO;

namespace Com.Ericmas001.Net.JSON
{
    public class JsonNumericValue : JsonObject
    {
        public override object GetValue()
        {
            return Value;
        }

        private double m_Value;

        public double Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        public JsonNumericValue()
        {
        }

        public JsonNumericValue(string name)
        {
            Name = name;
        }

        public JsonNumericValue(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public JsonNumericValue(double value)
        {
            Value = value;
        }

        public JsonNumericValue(int value)
        {
            Value = value;
        }

        public JsonNumericValue(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public JsonNumericValue(float value)
        {
            Value = value;
        }

        public JsonNumericValue(string name, float value)
        {
            Name = name;
            Value = value;
        }

        public JsonNumericValue(long value)
        {
            Value = value;
        }

        public JsonNumericValue(string name, long value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as JsonNumericValue;
            if (other == null)
            {
                return false;
            }

            return (Value == other.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override void WriteTo(TextWriter writer)
        {
            if (Name != string.Empty)
            {
                writer.Write(JsonUtility.QUOTE);
                writer.Write(Name);
                writer.Write(JsonUtility.QUOTE);
                writer.Write(JsonUtility.NAME_SEPARATOR);
                JsonUtility.WriteSpace(writer);
            }

            writer.Write(Value.ToString("g", JsonUtility.CultureInfo));
        }
    }
}