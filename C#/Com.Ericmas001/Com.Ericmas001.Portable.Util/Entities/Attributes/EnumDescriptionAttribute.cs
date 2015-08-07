using System;

namespace Com.Ericmas001.Portable.Util.Entities.Attributes
{
    public class EnumDescriptionAttribute : Attribute
    {
        public string DisplayName { get; private set; }

        public EnumDescriptionAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }
}
