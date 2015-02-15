using System;
using System.Collections.Generic;
using Com.Ericmas001.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Util.Entities.Filters.Attributes
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
