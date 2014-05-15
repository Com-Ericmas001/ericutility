using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    public static class EnumFactory<T> where T : struct
    {
        private static Dictionary<T, string> m_ToStringDic = null;
        private static Dictionary<string, T> m_ParsingDic = null;

        private static void Init()
        {
            if (!typeof (T).IsEnum)
                throw new Exception("<T> must be of Enum type (" + typeof (T).Name + ")");
            m_ParsingDic = new Dictionary<string, T>();
            m_ToStringDic = new Dictionary<T, string>();
            foreach (T e in Enum.GetValues(typeof(T)))
            {
                string desc = GetDescription(e);
                m_ParsingDic.Add(desc, e);
                m_ToStringDic.Add(e, desc);
            }
        }

        public static string ToString(T enumValue)
        {
            if (m_ToStringDic == null)
                Init();
            return m_ToStringDic[enumValue];
        }
        public static T Parse(string s)
        {
            if (m_ParsingDic == null)
                Init();

            if (!m_ParsingDic.ContainsKey(s))
                s = "";

            if (!m_ParsingDic.ContainsKey(s))
                return m_ParsingDic.Values.FirstOrDefault();

            return m_ParsingDic[s];
        }

        private static string GetDescription(T enumerationValue)
        {
            MemberInfo[] memberInfo = typeof(T).GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return enumerationValue.ToString();
        }
    }
}
