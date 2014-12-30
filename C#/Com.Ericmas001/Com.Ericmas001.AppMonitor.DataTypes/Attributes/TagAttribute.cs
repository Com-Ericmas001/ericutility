using System;

namespace Com.Ericmas001.AppMonitor.DataTypes.Attributes
{
    public class TagAttribute : Attribute
    {
        public string Tag { get; private set; }

        public TagAttribute(string tag)
        {
            Tag = tag;
        }
    }
}
