using System;

namespace Com.Ericmas001.Wpf.Validations
{
    public class CustomValidationAttribute : Attribute
    {
        private readonly string m_MethodName;

        public CustomValidationAttribute(string method)
        {
            m_MethodName = method;
        }

        public string Validate(object o, string value)
        {
            return ((Func<string, string>)Delegate.CreateDelegate(typeof(Func<string, string>), o, m_MethodName))(value);
        }
    }
}
