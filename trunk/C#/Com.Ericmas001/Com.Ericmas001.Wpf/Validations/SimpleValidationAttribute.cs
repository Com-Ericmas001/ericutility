using System;

namespace Com.Ericmas001.Wpf.Validations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public abstract class SimpleValidationAttribute : Attribute
    {
        public abstract string Validate(string value);
    }
}
