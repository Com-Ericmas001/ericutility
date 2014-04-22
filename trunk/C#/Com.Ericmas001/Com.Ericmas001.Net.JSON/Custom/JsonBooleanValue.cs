using System;
using System.IO;

namespace Com.Ericmas001.Net.JSON.Custom
{
    public class JsonBooleanValue : JsonObject
    {
        private bool? m_Value = new bool?();

        public bool? Value
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

        public JsonBooleanValue()
        {
        }

        public JsonBooleanValue(string name, string value)
        {
            Name = name;

            m_Value = null;

            if (value != null)
            {
                value = value.Trim().ToLower();
                if (value != string.Empty)
                {
                    switch (value.Trim().ToLower())
                    {
                        case "null":
                            m_Value = null;
                            break;

                        case "true":
                            m_Value = true;
                            break;

                        case "false":
                            m_Value = false;
                            break;

                        default:
                            throw new NotSupportedException();
                    }
                }
            }
        }

        public JsonBooleanValue(string name, bool? value)
        {
            Name = name;
            Value = value;
        }

        public JsonBooleanValue(bool? value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var other = obj as JsonBooleanValue;
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

            if (Value.HasValue)
            {
                writer.Write(Value.ToString().ToLower());
            }
            else
            {
                writer.Write("null");
            }
        }

        public override object GetValue()
        {
            return Value;
        }
    }
}