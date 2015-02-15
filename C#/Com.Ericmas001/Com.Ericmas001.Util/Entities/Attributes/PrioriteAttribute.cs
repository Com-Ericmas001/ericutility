using System;

namespace Com.Ericmas001.Util.Entities.Attributes
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
