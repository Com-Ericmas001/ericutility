using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Util.Entities.Attributes
{
    public class ColorAttribute : Attribute
    {
        public string Color { get; private set; }

        public ColorAttribute(string color)
        {
            Color = color;
        }
    }
}
