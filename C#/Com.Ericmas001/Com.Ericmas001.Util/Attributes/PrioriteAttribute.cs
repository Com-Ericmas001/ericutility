using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Ericmas001.Util.Attributes
{
    public class PriorityAttribute : Attribute
    {
        public int Priority { get; private set; }

        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}
