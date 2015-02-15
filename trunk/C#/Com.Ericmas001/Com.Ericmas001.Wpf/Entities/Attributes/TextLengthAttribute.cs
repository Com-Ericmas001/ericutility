using System;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.Wpf.Entities.Attributes
{
    public class TextLengthAttribute : Attribute
    {
        public TextLengthEnum Length { get; private set; }

        public TextLengthAttribute(TextLengthEnum length)
        {
            Length = length;
        }
    }
}
