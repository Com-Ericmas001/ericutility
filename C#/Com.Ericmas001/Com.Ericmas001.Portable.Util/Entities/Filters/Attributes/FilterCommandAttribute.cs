using System;
using System.Collections.Generic;
using Com.Ericmas001.Portable.Util.Entities.Filters.Enums;

namespace Com.Ericmas001.Portable.Util.Entities.Filters.Attributes
{
    public class FilterCommandAttribute : Attribute
    {
        public IEnumerable<FilterCommandEnum> Commands{get; private set; }
        public FilterCommandAttribute(params FilterCommandEnum[] commands)
        {
            Commands = commands;
        }

    }
}
