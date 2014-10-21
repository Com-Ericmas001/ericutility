using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ericmas001.Util
{
    public static class AttributeUtility
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector)
            where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() as TAttribute;
            if (att != null)
                return valueSelector(att);
            return default(TValue);
        }

        public static T GetEnumAttribute<T>(object myEnum)
            where T : Attribute
        {
            MemberInfo[] memberInfo = myEnum.GetType().GetMember(myEnum.ToString());
            if (memberInfo.Any())
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof (T), false);
                if (attrs.Any())
                    return (T)attrs.First();
            }
            return null;
        }
    }
}
