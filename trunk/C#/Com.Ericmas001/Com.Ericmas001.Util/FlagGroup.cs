using System;

namespace Com.Ericmas001.Util
{
    public class FlagGroup<T> where T : struct, IComparable, IFormattable, IConvertible
    {
        public FlagGroup()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }
        }
        private T m_Flag = (T)Enum.ToObject(typeof(T),0);

        public bool Allow(T f)
        {
            return (Convert.ToInt32(m_Flag) & Convert.ToInt32(f)) != 0;
        }

        public void SetFlag(T f, bool value)
        {
            var val = Convert.ToInt32(m_Flag);
            if (value)
                val |= Convert.ToInt32(f);
            else
                val &= ~Convert.ToInt32(f);
            m_Flag = (T)Enum.ToObject(typeof(T), val);
        }

        public T Flag { get { return m_Flag; } }
    }
}
