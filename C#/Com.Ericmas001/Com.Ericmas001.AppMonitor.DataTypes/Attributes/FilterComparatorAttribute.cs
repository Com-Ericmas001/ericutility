using System;
using System.Collections.Generic;
using Com.Ericmas001.AppMonitor.DataTypes.Enums;

namespace Com.Ericmas001.AppMonitor.DataTypes.Attributes
{
    public class FilterComparatorAttribute : Attribute
    {
        public IEnumerable<FilterComparatorEnum> Comparators { get; private set; }
        public FilterComparatorAttribute(params FilterComparatorEnum[] comparators)
        {
            Comparators = comparators;
        }
    }
}
