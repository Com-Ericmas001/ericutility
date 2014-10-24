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
        public static T[] AllValues = Enum.GetValues(typeof(T)).Cast<T>().ToArray();
       
        private static Dictionary<T, string> m_ToStringDic = null;
        private static Dictionary<string, T> m_ParsingDic = null;

        private static void Init()
        {
            if (!typeof(T).IsEnum)
                throw new Exception("<T> must be of Enum type (" + typeof(T).Name + ")");
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
            if (m_ToStringDic.ContainsKey(enumValue))
                return m_ToStringDic[enumValue];
            return enumValue.ToString();
        }
        public static T Parse(string s)
        {
            if (m_ParsingDic == null)
                Init();

            if (s == null)
                s = "";

            if (!m_ParsingDic.ContainsKey(s))
                return m_ParsingDic.Values.FirstOrDefault();

            return m_ParsingDic[s];
        }

        private static string GetDescription(T enumerationValue)
        {
            DescriptionAttribute da = GetAttribute<DescriptionAttribute>(enumerationValue);
            return da == null ? enumerationValue.ToString() : da.Description;
        }

        private static Dictionary<T, Dictionary<Type, Attribute>> m_Attributes = new Dictionary<T, Dictionary<Type, Attribute>>();

        public static TAtt GetAttribute<TAtt>(T enumerationValue)
            where TAtt : Attribute
        {
            if (!m_Attributes.ContainsKey(enumerationValue))
                m_Attributes.Add(enumerationValue, new Dictionary<Type, Attribute>());

            Type attType = typeof(TAtt);
            if (!m_Attributes[enumerationValue].ContainsKey(attType))
            {
                MemberInfo[] memberInfo = typeof(T).GetMember(enumerationValue.ToString());
                if (memberInfo.Any())
                {
                    object[] attrs = memberInfo[0].GetCustomAttributes(typeof(TAtt), false);
                    if (attrs.Any())
                        m_Attributes[enumerationValue].Add(attType, (TAtt)attrs.First());
                    else
                        m_Attributes[enumerationValue].Add(attType, null);
                }
                else
                    m_Attributes[enumerationValue].Add(attType, null);
            }
            return (TAtt)m_Attributes[enumerationValue][attType];
        }
    }
}
