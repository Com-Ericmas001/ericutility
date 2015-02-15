using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Com.Ericmas001.Util.Entities.Filters.Attributes;
using Com.Ericmas001.Util.Entities.Filters.Commands;
using Com.Ericmas001.Util.Entities.Filters.Comparators;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Util.Entities.Fields
{
    public abstract class SimpleField : BaseField
    {
        private class SimpleFieldCtor
        {
            public Type CtorType { get; private set; }

            public SimpleFieldCtor(Type t)
            {
                CtorType = t;
            }

            public SimpleField Invoke()
            {
                return CtorType.GetConstructor(new Type[] { }).Invoke(new object[0]) as SimpleField; 
            }
        }

        private static Dictionary<FieldTypeEnum, SimpleFieldCtor> m_AllFieldTypes;

        public static SimpleField GenerateField(FieldTypeEnum typeEnum)
        {
            if (m_AllFieldTypes == null)
            {
                m_AllFieldTypes = new Dictionary<FieldTypeEnum, SimpleFieldCtor>();
                foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(SimpleField))))
                    m_AllFieldTypes.Add(type.GetAttributeValue((FieldTypeAttribute att) => att.FieldType), new SimpleFieldCtor(type));
            }
            if (!m_AllFieldTypes.ContainsKey(typeEnum))
                return null;
            return m_AllFieldTypes[typeEnum].Invoke();
        }
    }
}
