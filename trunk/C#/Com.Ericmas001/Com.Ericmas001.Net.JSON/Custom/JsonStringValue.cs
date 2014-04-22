using System.IO;

namespace Com.Ericmas001.Net.JSON.Custom
{
    public class JsonStringValue : JsonObject
    {
        public override object GetValue()
        {
            return Value;
        }

        private string m_Value;

        public string Value
        {
            get
            {
                if (m_Value != null)
                {
                    return m_Value;
                }
                return string.Empty;
            }
            set
            {
                m_Value = value;
            }
        }

        public JsonStringValue()
        {
        }

        public JsonStringValue(string name)
        {
            Name = name;
        }

        public JsonStringValue(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as JsonStringValue;
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

            writer.Write(JsonUtility.EscapeString(Value));
        }
    }
}