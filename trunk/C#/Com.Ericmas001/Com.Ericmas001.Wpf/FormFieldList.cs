using System;
using System.Linq;

namespace Com.Ericmas001.Wpf
{
    public class FormFieldList<TEnum> : FormFieldList<TEnum, string>
        where TEnum : struct
    {
        private FormField<TEnum>[] m_Fields = FormField<TEnum>.AllFields;

        public new FormField<TEnum>[] Fields { get { return m_Fields; } }

        public new FormField<TEnum> GetField(TEnum key)
        {
            return m_Fields.First(x => x.Field.Equals(key));
        }

    }
    public class FormFieldList<TEnum, TValue>
        where TEnum : struct
    {
        private FormField<TEnum, TValue>[] m_Fields = FormField<TEnum, TValue>.AllFields;

        public FormField<TEnum, TValue>[] Fields { get { return m_Fields; } }

        public FormFieldList()
        {
            if (!typeof(TEnum).IsEnum)
                throw new Exception("<TEnum> must be of Enum type (" + typeof(TEnum).Name + ")");
        }

        public FormField<TEnum, TValue> GetField(TEnum key)
        {
            return m_Fields.First(x => x.Field.Equals(key));
        }

    }
}
