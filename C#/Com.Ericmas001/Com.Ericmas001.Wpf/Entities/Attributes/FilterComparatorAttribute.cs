using System;
using System.Collections.Generic;
using Com.Ericmas001.Wpf.Entities.Enums;

namespace Com.Ericmas001.Wpf.Entities.Attributes
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
